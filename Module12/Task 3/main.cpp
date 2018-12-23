#include<Windows.h>    
// first include Windows.h header file which is required    
#include<stdio.h>
#include "glew.h"
#include<gl/GL.h>   // GL.h header file    
#include<gl/GLU.h> // GLU.h header file    
#include <freeglut.h>  // glut.h header file from freeglut\include\GL folder    
#include<conio.h>
#include<math.h>
#include<string>
#include <vector>
#include <glm.hpp>
#include <string>
#include <sstream>
#include <fstream>
#include <iostream>
#include <map>
#include <SOIL.h>
#include <gtc/matrix_transform.hpp>
#include "GlShader.h"

using namespace std;

std::string objname = "C:\\Users\\Эллоне\\Desktop\\cubetext.obj";

string vsPath = "C:\\Users\\Эллоне\\Desktop\\Task 3\\vertex.shader";
string fsPath = "C:\\Users\\Эллоне\\Desktop\\Task 3\\fragment_oneText.shader";

string texPath = "D:\\4nqnbO1WtYI.jpg";
//string texPath = "road.bmp";


string loadFile(string path) {
	ifstream fs(path, ios::in);
	if (!fs) cerr << "Cannot open " << path << endl;
	string fsS;
	while (getline(fs, fsS, '\0'))
		cout << fsS << endl;
	return fsS;
}

GLuint textureID;

void _LoadImage() {
	textureID = SOIL_load_OGL_texture(texPath.c_str(), SOIL_LOAD_AUTO, SOIL_CREATE_NEW_ID, SOIL_FLAG_MIPMAPS | SOIL_FLAG_INVERT_Y | SOIL_FLAG_NTSC_SAFE_RGB | SOIL_FLAG_COMPRESS_TO_DXT);
}

// Наш класс шейдера
GlShader shader;
//! Переменные с индентификаторами ID
//! ID атрибута вершин
GLint  Attrib_vertex;

GLint  Attrib_normal;

//! ID атрибута цветов
GLint  Unif_color;
//! ID юниформ матрицы проекции
GLint  Unif_matrix;
//! Матрица проекции
mat4 Matrix_projection;

GLint smp;

std::vector<glm::vec3> indexed_vertices;
std::vector<glm::vec2> indexed_uvs;
std::vector<glm::vec3> indexed_normals;
std::vector<unsigned short> indices;
GLuint vertexbuffer;
GLuint uvbuffer;
GLuint normalbuffer;
GLuint elementbuffer;

//! Вершина
struct vertex
{
	GLfloat x;
	GLfloat y;
	GLfloat z;
};

//! Инициализация OpenGL, здесь пока по минимальному
void initGL()
{
	glClearColor(0, 0, 0, 0);
	glEnable(GL_DEPTH_TEST);
}

//! Проверка ошибок OpenGL, если есть то выводит в консоль тип ошибки
void checkOpenGLerror()
{
	GLenum errCode;
	if ((errCode = glGetError()) != GL_NO_ERROR)
		std::cout << "OpenGl error! - " << gluErrorString(errCode);
}

//! Инициализация шейдеров
void initShader()
{
	string vsSource = loadFile(vsPath), fsSource = loadFile(fsPath);

	if (!shader.load(vsSource, fsSource))
	{
		std::cout << "error load shader \n";
		return;
	}

	///! Вытягиваем ID атрибута из собранной программы 
	//Attrib_vertex = shader.getAttribLocation("coord");

	//Attrib_normal = shader.getAttribLocation("normal");

	//! Вытягиваем ID юниформ
	//Unif_color = shader.getUniformLocation("fragmentColor");

	//! Вытягиваем ID юниформ матрицы проекции
	Unif_matrix = shader.getUniformLocation("matrix");

	smp = shader.getUniformLocation("myTextureSampler");

	checkOpenGLerror();
}

void loadOBJ(const std::string & path, std::vector<glm::vec3> & out_vertices, std::vector<glm::vec2> & out_uvs, std::vector<glm::vec3> & out_normals)
{
	std::vector<unsigned int> vertex_indices, uv_indices, normal_indices;
	std::vector<glm::vec3> temp_vertices;
	std::vector<glm::vec2> temp_uvs;
	std::vector<glm::vec3> temp_normals;

	std::ifstream infile(path);
	std::string line;
	
	int obj_scale = 1;
	
	while (getline(infile, line))
	{
		std::stringstream ss(line);
		std::string lineHeader;
		getline(ss, lineHeader, ' ');
		if (lineHeader == "v")
		{
			glm::vec3 vertex;
			ss >> vertex.x >> vertex.y >> vertex.z;

			vertex.x *= obj_scale;
			vertex.y *= obj_scale;
			vertex.z *= obj_scale;
			temp_vertices.push_back(vertex);
		}
		else if (lineHeader == "vt")
		{
			glm::vec2 uv;
			ss >> uv.x >> uv.y;
			temp_uvs.push_back(uv);
		}
		else if (lineHeader == "vn")
		{
			glm::vec3 normal;
			ss >> normal.x >> normal.y >> normal.z;
			//normal.x *= -1;
			//normal.y *= -1;
			//normal.z *= -1;
			temp_normals.push_back(normal);
		}
		else if (lineHeader == "f")
		{
			int vertex_index, uv_index, normal_index;
			char slash;
			while (ss >> vertex_index >> slash >> uv_index >> slash >> normal_index)
			{
				vertex_indices.push_back(vertex_index);
				uv_indices.push_back(uv_index);
				normal_indices.push_back(normal_index);
			}
		}
	}

	// For each vertex of each triangle
	for (unsigned int i = 0; i < vertex_indices.size(); i++)
	{
		unsigned int vertexIndex = vertex_indices[i];
		glm::vec3 vertex = temp_vertices[vertexIndex - 1];
		out_vertices.push_back(vertex);

		unsigned int uvIndex = uv_indices[i];
		glm::vec2 uv = temp_uvs[uvIndex - 1];
		out_uvs.push_back(uv);

		unsigned int normalIndex = normal_indices[i];
		glm::vec3 normal = temp_normals[normalIndex - 1];
		out_normals.push_back(normal);
	}
}

struct PackedVertex
{
	glm::vec3 position;
	glm::vec2 uv;
	glm::vec3 normal;
	bool operator<(const PackedVertex that) const
	{
		return memcmp((void*)this, (void*)&that, sizeof(PackedVertex)) > 0;
	};
};

bool getSimilarVertexIndex_fast(
	PackedVertex & packed,
	std::map<PackedVertex, unsigned short> & VertexToOutIndex,
	unsigned short & result
)
{
	std::map<PackedVertex, unsigned short>::iterator it = VertexToOutIndex.find(packed);
	if (it == VertexToOutIndex.end())
	{
		return false;
	}
	else
	{
		result = it->second;
		return true;
	}
}

GLuint VBO, VAO, IBO;

void indexVBO(
	std::vector<glm::vec3> & in_vertices,
	std::vector<glm::vec2> & in_uvs,
	std::vector<glm::vec3> & in_normals,

	std::vector<unsigned short> & out_indices,
	std::vector<glm::vec3> & out_vertices,
	std::vector<glm::vec2> & out_uvs,
	std::vector<glm::vec3> & out_normals
)
{
	std::map<PackedVertex, unsigned short> VertexToOutIndex;

	// For each input vertex
	for (unsigned int i = 0; i < in_vertices.size(); i++)
	{
		PackedVertex packed = { in_vertices[i], in_uvs[i], in_normals[i] }; // 

		// Try to find a similar vertex in out_XXXX
		unsigned short index;
		bool found = getSimilarVertexIndex_fast(packed, VertexToOutIndex, index);

		if (found)
		{ // A similar vertex is already in the VBO, use it instead!
			out_indices.push_back(index);
		}
		else
		{ // If not, it needs to be added in the output data.
			out_vertices.push_back(in_vertices[i]);
			out_uvs.push_back(in_uvs[i]);
			out_normals.push_back(in_normals[i]);
			unsigned short newindex = (unsigned short)out_vertices.size() - 1;
			out_indices.push_back(newindex);
			VertexToOutIndex[packed] = newindex;
		}
	}
}

//! Инициализация VBO_vertex
void initVBO()
{
	// Read our .obj file
	std::vector<glm::vec3> vertices;
	std::vector<glm::vec2> uvs;
	std::vector<glm::vec3> normals;
	loadOBJ(objname.c_str(), vertices, uvs, normals);
	indexVBO(vertices, uvs, normals, indices, indexed_vertices, indexed_uvs, indexed_normals);

	cout << indexed_vertices.size();

	glGenBuffers(1, &vertexbuffer);
	glGenBuffers(1, &uvbuffer);
	glGenBuffers(1, &normalbuffer);
	glGenBuffers(1, &elementbuffer);
	glGenVertexArrays(1, &VAO);

	glBindVertexArray(VAO);
	//binding
	glBindBuffer(GL_ARRAY_BUFFER, vertexbuffer);
	glBufferData(GL_ARRAY_BUFFER, indexed_vertices.size() * sizeof(glm::vec3), &indexed_vertices[0], GL_STATIC_DRAW);

	glBindBuffer(GL_ARRAY_BUFFER, uvbuffer);
	glBufferData(GL_ARRAY_BUFFER, indexed_uvs.size() * sizeof(glm::vec2), &indexed_uvs[0], GL_STATIC_DRAW);

	glBindBuffer(GL_ARRAY_BUFFER, normalbuffer);
	glBufferData(GL_ARRAY_BUFFER, indexed_normals.size() * sizeof(glm::vec3), &indexed_normals[0], GL_STATIC_DRAW);

	glBindBuffer(GL_ELEMENT_ARRAY_BUFFER, elementbuffer);
	glBufferData(GL_ELEMENT_ARRAY_BUFFER, indices.size() * sizeof(unsigned short), &indices[0], GL_STATIC_DRAW);

	glEnableVertexAttribArray(0);
	glBindBuffer(GL_ARRAY_BUFFER, vertexbuffer);
	glVertexAttribPointer(0, 3, GL_FLOAT, GL_FALSE, 0, 0);
	
	glEnableVertexAttribArray(1);
	glBindBuffer(GL_ARRAY_BUFFER, uvbuffer);
	glVertexAttribPointer(1, 2, GL_FLOAT, GL_FALSE, 0, 0);
	


	// 3rd attribute buffer : normals
	//glBindBuffer(GL_ARRAY_BUFFER, normalbuffer);
	//glVertexAttribPointer(Attrib_normal, 3, GL_FLOAT, GL_FALSE, 0, 0);
	//glEnableVertexAttribArray(Attrib_normal);


	checkOpenGLerror();
}

void freeVBO()
{
	glBindBuffer(GL_ARRAY_BUFFER, 0);
	glBindBuffer(GL_ELEMENT_ARRAY_BUFFER, 0);
}

void resizeWindow(int width, int height)
{
	glViewport(0, 0, width, height);

	height = height > 0 ? height : 1;
	const GLfloat aspectRatio = (GLfloat)width / (GLfloat)height;

	Matrix_projection = glm::perspective(45.0f, aspectRatio, 0.00001f, 500.0f);
	// Перемещаем центр нашей оси координат для того чтобы увидеть куб
	Matrix_projection = glm::translate(Matrix_projection, vec3(-1.0f, 0.0f, -10.0f));
	// Поворачиваем ось координат(тоесть весь мир), чтобы развернуть отрисованное
	Matrix_projection = glm::rotate(Matrix_projection, 100.0f, vec3(1.0f, 1.0f, 0.0f));

	glm::mat4 View = glm::lookAt(
		glm::vec3(1, 0, 3),
		glm::vec3(0, 0, 0),
		glm::vec3(0, 1, 0)
	);

	Matrix_projection *= View;
}

//! Отрисовка
void render()
{
	glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);

	//! Устанавливаем шейдерную программу текущей
	shader.use();
	//! Передаем матрицу в шейдер
	shader.setUniform(Unif_matrix, Matrix_projection);

	glActiveTexture(GL_TEXTURE0);
	glBindTexture(GL_TEXTURE_2D, textureID);
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_NEAREST);
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_NEAREST);

	glEnable(GL_TEXTURE_2D);

	shader.setUniform(smp, textureID);


	glBindVertexArray(VAO);
	glDrawElements(GL_TRIANGLES, indices.size(), GL_UNSIGNED_SHORT, 0);

	checkOpenGLerror();

	glutSwapBuffers();
}

int main(int argc, char **argv)
{
	glutInit(&argc, argv);
	glutInitDisplayMode(GLUT_RGBA | GLUT_ALPHA | GLUT_DOUBLE);
	glutInitWindowSize(800, 600);
	glutCreateWindow("Simple shaders");

	//! Обязательно перед инициализации шейдеров
	GLenum glew_status = glewInit();
	if (GLEW_OK != glew_status)
	{
		//! GLEW не проинициализировалась
		std::cout << "Error: " << glewGetErrorString(glew_status) << "\n";
		return 1;
	}

	//! Проверяем доступность OpenGL 2.0
	if (!GLEW_VERSION_2_0)
	{
		//! OpenGl 2.0 оказалась не доступна
		std::cout << "No support for OpenGL 2.0 found\n";
		return 1;
	}

	//! Инициализация
	initGL();
	_LoadImage();
	initVBO();
	initShader();

	glutReshapeFunc(resizeWindow);
	glutDisplayFunc(render);
	glutMainLoop();

	//! Освобождение ресурсов, хотя в нашем случаи сюда выполнение никогда не дойдет,
	// так, как управление не выйдет из glutMainLoop цикла
	freeVBO();
}
