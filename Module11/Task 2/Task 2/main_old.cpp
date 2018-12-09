#include <GL\glew.h>
#include <GL\freeglut.h>
#include <string>
#include <vector>
#include <vec3.hpp>
#include <vec2.hpp>
#include <Importer.hpp>
#include <scene.h>
#include <postprocess.h>
#include <iostream>
#include <SOIL.h>

#define _CRT_SECURE_NO_WARNINGS

using std::string;
using std::vector;
using glm::vec2;
using glm::vec3;
using std::cout;

static int w = 800, h = 800;

struct Vertex {
	glm::vec3 Position;
	glm::vec3 Normal;
	glm::vec2 TexCoords;
};

struct Texture {
	unsigned int id;
	string type;
	aiString path;  // храним путь к текстуре для нужд сравнения объектов текстур
};

class Mesh {
public:
	/*  Mesh Data  */
	vector<Vertex> vertices;
	vector<unsigned int> indices;
	vector<Texture> textures;
	/*  Functions  */
	Mesh(vector<Vertex> vertices, vector<unsigned int> indices, vector<Texture> textures)
	{
		this->vertices = vertices;
		this->indices = indices;
		this->textures = textures;

		setupMesh();
	}
	/*void Draw(Shader shader)
	{
		unsigned int diffuseNr = 1;
		unsigned int specularNr = 1;
		for (unsigned int i = 0; i < textures.size(); i++)
		{
			glActiveTexture(GL_TEXTURE0 + i); // активируем текстурный блок, до привязки
			// получаем номер текстуры
			stringstream ss;
			string number;
			string name = textures[i].type;
			if (name == "texture_diffuse")
				ss << diffuseNr++; // передаем unsigned int в stream
			else if (name == "texture_specular")
				ss << specularNr++; // передаем unsigned int в stream
			number = ss.str();

			shader.setFloat(("material." + name + number).c_str(), i);
			glBindTexture(GL_TEXTURE_2D, textures[i].id);
		}
		glActiveTexture(GL_TEXTURE0);

		// отрисовывем полигональную сетку
		glBindVertexArray(VAO);
		glDrawElements(GL_TRIANGLES, indices.size(), GL_UNSIGNED_INT, 0);
		glBindVertexArray(0);
	}*/

private:
	/*  Render data  */
	unsigned int VAO, VBO, EBO;
	/*  Functions    */
	void setupMesh()
	{
		glGenVertexArrays(1, &VAO);
		glGenBuffers(1, &VBO);
		glGenBuffers(1, &EBO);

		glBindVertexArray(VAO);
		glBindBuffer(GL_ARRAY_BUFFER, VBO);

		glBufferData(GL_ARRAY_BUFFER, vertices.size() * sizeof(Vertex), &vertices[0], GL_STATIC_DRAW);

		glBindBuffer(GL_ELEMENT_ARRAY_BUFFER, EBO);
		glBufferData(GL_ELEMENT_ARRAY_BUFFER, indices.size() * sizeof(unsigned int),
			&indices[0], GL_STATIC_DRAW);

		// vertex positions
		glEnableVertexAttribArray(0);
		glVertexAttribPointer(0, 3, GL_FLOAT, GL_FALSE, sizeof(Vertex), (void*)0);
		// vertex normals
		glEnableVertexAttribArray(1);
		glVertexAttribPointer(1, 3, GL_FLOAT, GL_FALSE, sizeof(Vertex), (void*)offsetof(Vertex, Normal));
		// vertex texture coords
		glEnableVertexAttribArray(2);
		glVertexAttribPointer(2, 2, GL_FLOAT, GL_FALSE, sizeof(Vertex), (void*)offsetof(Vertex, TexCoords));

		glBindVertexArray(0);
	}
};

class Model
{
public:
	/*  Методы   */
	Model(char *path)
	{
		loadModel(path);
	}
	/*void Draw(Shader shader)
	{
		for (unsigned int i = 0; i < meshes.size(); i++)
			meshes[i].Draw(shader);
	}*/
private:
	/*  Данные модели  */
	vector<Mesh> meshes;
	string directory;
	/*  Методы   */
	void loadModel(string path)
	{
		Assimp::Importer import;
		const aiScene *scene = import.ReadFile(path, aiProcess_Triangulate | aiProcess_FlipUVs);

		if (!scene || scene->mFlags & AI_SCENE_FLAGS_INCOMPLETE || !scene->mRootNode)
		{
			cout << "ERROR::ASSIMP::" << import.GetErrorString() << std::endl;
			return;
		}
		directory = path.substr(0, path.find_last_of('/'));

		processNode(scene->mRootNode, scene);
	}
	void processNode(aiNode *node, const aiScene *scene)
	{
		// обработать все полигональные сетки в узле(если есть)
		for (unsigned int i = 0; i < node->mNumMeshes; i++)
		{
			aiMesh *mesh = scene->mMeshes[node->mMeshes[i]];
			meshes.push_back(processMesh(mesh, scene));
		}
		// выполнить ту же обработку и для каждого потомка узла
		for (unsigned int i = 0; i < node->mNumChildren; i++)
		{
			processNode(node->mChildren[i], scene);
		}
	}
	Mesh processMesh(aiMesh *mesh, const aiScene *scene)
	{
		vector<Vertex> vertices;
		vector<unsigned int> indices;
		vector<Texture> textures;

		for (unsigned int i = 0; i < mesh->mNumVertices; i++)
		{
			Vertex vertex;
			// обработка координат, нормалей и текстурных координат вершин
			glm::vec3 vector;
			vector.x = mesh->mVertices[i].x;
			vector.y = mesh->mVertices[i].y;
			vector.z = mesh->mVertices[i].z;
			vertex.Position = vector;
			vector.x = mesh->mNormals[i].x;
			vector.y = mesh->mNormals[i].y;
			vector.z = mesh->mNormals[i].z;
			vertex.Normal = vector;
			if (mesh->mTextureCoords[0]) // сетка обладает набором текстурных координат?
			{
				glm::vec2 vec;
				vec.x = mesh->mTextureCoords[0][i].x;
				vec.y = mesh->mTextureCoords[0][i].y;
				vertex.TexCoords = vec;
			}
			else
				vertex.TexCoords = glm::vec2(0.0f, 0.0f);

			vertices.push_back(vertex);
		}
		// орбаботка индексов
		for (unsigned int i = 0; i < mesh->mNumFaces; i++)
		{
			aiFace face = mesh->mFaces[i];
			for (unsigned int j = 0; j < face.mNumIndices; j++)
				indices.push_back(face.mIndices[j]);
		}
			// обработка материала
			/*if (mesh->mMaterialIndex >= 0)
			{
				if (mesh->mMaterialIndex >= 0)
				{
					aiMaterial *material = scene->mMaterials[mesh->mMaterialIndex];
					vector<Texture> diffuseMaps = loadMaterialTextures(material,
						aiTextureType_DIFFUSE, "texture_diffuse");
					textures.insert(textures.end(), diffuseMaps.begin(), diffuseMaps.end());
					vector<Texture> specularMaps = loadMaterialTextures(material,
						aiTextureType_SPECULAR, "texture_specular");
					textures.insert(textures.end(), specularMaps.begin(), specularMaps.end());
				}
			}*/

		return Mesh(vertices, indices, textures);
	}
	/*vector<Texture> loadMaterialTextures(aiMaterial *mat, aiTextureType type, string typeName)
	{
		vector<Texture> textures;
		for (unsigned int i = 0; i < mat->GetTextureCount(type); i++)
		{
			aiString str;
			mat->GetTexture(type, i, &str);
			bool skip = false;
			for (unsigned int j = 0; j < textures_loaded.size(); j++)
			{
				if (std::strcmp(textures_loaded[j].path.C_Str(), str.C_Str()) == 0)
				{
					textures.push_back(textures_loaded[j]);
					skip = true;
					break;
				}
			}
			if (!skip)
			{   // если текстура не была загружена – сделаем это
				Texture texture;
				texture.id = TextureFromFile(str.C_Str(), directory);
				texture.type = typeName;
				texture.path = str;
				textures.push_back(texture);
				// занесем текстуру в список уже загруженных
				textures_loaded.push_back(texture);
			}
		}
		return textures;
	}*/
};


void Init(void) {
	glClearColor(0.0f, 0.0f, 0.0f, 1.0f);

	glEnable(GL_DEPTH_TEST);
	glEnable(GL_LIGHTING);
	glEnable(GL_COLOR_MATERIAL);
	glEnable(GL_LIGHT0);
	glLightModelf(GL_LIGHT_MODEL_TWO_SIDE, GL_TRUE);
	glEnable(GL_NORMALIZE);
}

void Update(void) {
	glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);
	glutSwapBuffers();
}

void Reshape(int width, int height) {
	width = w; height = h;
}

void drawWireCube() {
	glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);

	glutSolidCube(0.1);

	glFlush();
	glutSwapBuffers();
}

GLuint Program; //! ID юниформ переменной цвета 
GLint  Unif_color; 

void checkOpenGLerror() { 
	GLenum errCode;  
	if ((errCode = glGetError()) != GL_NO_ERROR)   
		std::cout << "OpenGl error! - " << gluErrorString(errCode); 
}

void initShader() {  //! Исходный код шейдеров  
	const char* vsSource =   "attribute vec3 coord;\n"   "void main() {\n"   "  gl_Position = vec4(coord, 1.0);\n"   "}\n";  
	const char* fsSource =   "uniform vec4 color;\n"   "void main() {\n"   "  gl_FragColor = color;\n"   "}\n"; 

 //! Переменные для хранения идентификаторов шейдеров  
	GLuint fShader; 
	GLuint vShader;
 //! Создаем фрагментный шейдер  
	vShader = glCreateShader(GL_VERTEX_SHADER);  //! Передаем исходный код  
	glShaderSource(vShader, 1, &vsSource, NULL);  
	//! Компилируем шейдер  
	glCompileShader(vShader); 

 //! Создаем программу и прикрепляем шейдеры к ней  
	Program = glCreateProgram();  glAttachShader(Program, vShader); 

 //! Линкуем шейдерную программу  
	glLinkProgram(Program); 

 //! Проверяем статус сборки  
	int link_ok;  glGetProgramiv(Program, GL_LINK_STATUS, &link_ok);  
	if (!link_ok)  {   std::cout << "error attach shaders \n";   return;  } 

 //! Вытягиваем ID юниформ  
	//const char* unif_name = "color";  
	//Unif_color = glGetUniformLocation(Program, unif_name);  
	//if (Unif_color == -1)  {   std::cout << "could not bind uniform " << unif_name << std::endl;   return;  }  checkOpenGLerror(); 
} 


int main(int argc, char** argv) {
	glutInit(&argc, argv);
	glutInitWindowPosition(250, 0);
	glutInitWindowSize(w, h);
	glutInitDisplayMode(GLUT_RGBA | GLUT_DOUBLE | GLUT_DEPTH);
	glutCreateWindow("OpenGL");

	glutReshapeFunc(Reshape);
	glutDisplayFunc(drawWireCube);

	Init();

	glutMainLoop();
}