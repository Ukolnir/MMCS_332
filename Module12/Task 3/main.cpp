#define _CRT_SECURE_NO_WARNINGS
#include <iostream>
#include <Windows.h>
#include "glew.h"
#include "wglew.h"
#include "freeglut.h"
#include <string>
#include <vector>
#include <fstream>
#include "SOIL.h"
#define GLM_ENABLE_EXPERIMENTAL
#include "glm.hpp"
#include <gtc/matrix_transform.hpp>
#include <gtx/transform.hpp>
#include <sstream>
#include <map>
using namespace std;

//-----------------------------SHADER_MODE
//-------------------------- 0 - oneTexture, 1 - mix with Color, 2 - twoTextures
int shader_mode = 0;

//-----------------------------TEXTURES AND MODELS

string texPath3 = "D:\\4nqnbO1WtYI.jpg";
//string texPath3 = "D:\\lotus_textures\\lotus_petal_diffuse.jpg"; 
//string objname = "C:\\Users\\Ёллоне\\Desktop\\alien glyder\\alien glyder.obj";

string objname = "C:\\Users\\Ёллоне\\Desktop\\lotus_OBJ_low.obj";


//---------------------------MOVES

float X = 0.0f, Y = 0.0f, Z = 0.0f;

float angle_X = 0.0f;
float angle_Y = 0.0f;
float angle_Z = 0.0f;

///----------------------------------------------------------------------------
GLuint textureID;

void _LoadImage() {
	textureID = SOIL_load_OGL_texture(texPath3.c_str(), SOIL_LOAD_AUTO, SOIL_CREATE_NEW_ID, SOIL_FLAG_MIPMAPS | SOIL_FLAG_INVERT_Y | SOIL_FLAG_NTSC_SAFE_RGB | SOIL_FLAG_COMPRESS_TO_DXT);
}

string vsPath = "C:\\Users\\Ёллоне\\Desktop\\Task 3\\vertex.shader";
string fsPath1 = "C:\\Users\\Ёллоне\\Desktop\\Task 3\\fragment_oneText.shader";
string fsPath2 = "C:\\Users\\Ёллоне\\Desktop\\Task 3\\fragment_mixColor.shader";
string fsPath3 = "C:\\Users\\Ёллоне\\Desktop\\Task 3\\fragment_twoText.shader";

int w, h;
GLuint Program;

string loadFile(string path) {
	ifstream fs(path, ios::in);
	if (!fs) cerr << "Cannot open " << path << endl;
	string fsS;
	while (getline(fs, fsS, '\0'))
		cout << fsS << endl;
	return fsS;
}

void checkOpenGLerror(){
	GLenum errCode;
	if ((errCode = glGetError()) != GL_NO_ERROR)
		cout << "OpenGl error! - " << gluErrorString(errCode);
}

void initShader() {
	string _f = loadFile(vsPath);
	const char* vsSource = _f.c_str();

	GLuint vShader, fShader;

	vShader = glCreateShader(GL_VERTEX_SHADER);
	glShaderSource(vShader, 1, &vsSource, NULL);
	glCompileShader(vShader);

	_f = loadFile(fsPath1);
	const char* fsSource = _f.c_str();

	fShader = glCreateShader(GL_FRAGMENT_SHADER);
	glShaderSource(fShader, 1, &fsSource, NULL);
	glCompileShader(fShader);

	//----

	Program = glCreateProgram();
	glAttachShader(Program, vShader);
	glAttachShader(Program, fShader);

	glLinkProgram(Program);
	int link_ok;
	glGetProgramiv(Program, GL_LINK_STATUS, &link_ok);
	if (!link_ok) { std::cout << "error attach shaders \n";   return; }

	checkOpenGLerror();
}

void freeShader() {
	glUseProgram(0);
	glDeleteProgram(Program);
}

void resizeWindow(int width, int height) { glViewport(0, 0, width, height); }

GLuint colorbuffer;
GLuint texturebuffer;

void loadOBJ(const std::string & path, std::vector<glm::vec3> & out_vertices, std::vector<glm::vec2> & out_uvs, std::vector<glm::vec3> & out_normals)
{
	std::vector<unsigned int> vertex_indices, uv_indices, normal_indices;
	std::vector<glm::vec3> temp_vertices;
	std::vector<glm::vec2> temp_uvs;
	std::vector<glm::vec3> temp_normals;

	std::ifstream infile(path);
	std::string line;

	int obj_scale = 1;

	while (getline(infile, line)){
		std::stringstream ss(line);
		std::string lineHeader;
		getline(ss, lineHeader, ' ');
		if (lineHeader == "v"){
			glm::vec3 vertex;
			ss >> vertex.x >> vertex.y >> vertex.z;

			vertex.x *= obj_scale;
			vertex.y *= obj_scale;
			vertex.z *= obj_scale;
			temp_vertices.push_back(vertex);
		}
		else if (lineHeader == "vt"){
			glm::vec2 uv;
			ss >> uv.x >> uv.y;
			temp_uvs.push_back(uv);
		}
		else if (lineHeader == "vn"){
			glm::vec3 normal;
			ss >> normal.x >> normal.y >> normal.z;
			//normal.x *= -1;
			//normal.y *= -1;
			//normal.z *= -1;
			temp_normals.push_back(normal);
		}
		else if (lineHeader == "f"){
			int vertex_index, uv_index, normal_index;
			char slash;
			while (ss >> vertex_index >> slash >> uv_index >> slash >> normal_index){
				vertex_indices.push_back(vertex_index);
				uv_indices.push_back(uv_index);
				normal_indices.push_back(normal_index);
			}
		}
	}

	// For each vertex of each triangle
	for (unsigned int i = 0; i < vertex_indices.size(); i++){
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
){
	std::map<PackedVertex, unsigned short>::iterator it = VertexToOutIndex.find(packed);
	if (it == VertexToOutIndex.end()) return false;
	else{
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
){
	std::map<PackedVertex, unsigned short> VertexToOutIndex;

	// For each input vertex
	for (unsigned int i = 0; i < in_vertices.size(); i++)
	{
		PackedVertex packed = { in_vertices[i], in_uvs[i], in_normals[i] };

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

std::vector<glm::vec3> indexed_vertices;
std::vector<glm::vec2> indexed_uvs;
std::vector<glm::vec3> indexed_normals;
std::vector<unsigned short> indices;
GLuint vertexbuffer;
GLuint uvbuffer;
GLuint normalbuffer;
GLuint elementbuffer;

void initVBO() {
	std::vector<glm::vec3> vertices;
	std::vector<glm::vec2> uvs;
	std::vector<glm::vec3> normals;
	loadOBJ(objname.c_str(), vertices, uvs, normals);
	indexVBO(vertices, uvs, normals, indices, indexed_vertices, indexed_uvs, indexed_normals);
	
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
	glBufferData(GL_ARRAY_BUFFER, indexed_normals.size() * sizeof(glm::vec2), &indexed_normals[0], GL_STATIC_DRAW);

	glBindBuffer(GL_ELEMENT_ARRAY_BUFFER, elementbuffer);
	glBufferData(GL_ELEMENT_ARRAY_BUFFER, indices.size() * sizeof(unsigned short), &indices[0], GL_STATIC_DRAW);
}

void render1() {
	glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);
	glm::mat4 Projection = glm::perspective(glm::radians(45.0f), 4.0f / 3.0f, 0.1f, 1000.0f);
	glm::mat4 View = glm::lookAt(
		glm::vec3(4, 7, -5),
		glm::vec3(0, 0, 0), 
		glm::vec3(0, 1, 0) 
	);

	glm::mat4 Model;

	Model = glm::translate(glm::mat4(1.0f), glm::vec3(X, Y, Z));
	Model = glm::rotate(Model, angle_X, glm::vec3(1.0f, 0.0f, 0.0f));
	Model = glm::rotate(Model, angle_Y, glm::vec3(0.0f, 1.0f, 0.0f));
	Model = glm::rotate(Model, angle_Z, glm::vec3(0.0f, 0.0f, 1.0f));

	glm::mat4 MVP = Projection * View * Model;

	glUseProgram(Program);

	glEnableVertexAttribArray(0);
	glBindBuffer(GL_ARRAY_BUFFER, vertexbuffer);
	glVertexAttribPointer(
		0,   
		3,             
		GL_FLOAT,          
		GL_FALSE,        
		0,              
		(void*)0        
	);

	glEnableVertexAttribArray(1);
	glBindBuffer(GL_ARRAY_BUFFER, uvbuffer);
	glVertexAttribPointer(
		1,
		2,
		GL_FLOAT,
		GL_FALSE,
		0,
		(void*)0
	);

	glEnableVertexAttribArray(2);
	glBindBuffer(GL_ARRAY_BUFFER, normalbuffer);
	glVertexAttribPointer(
		2,
		3,
		GL_FLOAT,
		GL_FALSE,
		0,
		(void*)0
	);

	GLuint MatrixID = glGetUniformLocation(Program, "MVP");
	glUniformMatrix4fv(MatrixID, 1, GL_FALSE, &MVP[0][0]);

	glActiveTexture(GL_TEXTURE0);
	glBindTexture(GL_TEXTURE_2D, textureID);
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_NEAREST);
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_NEAREST);

	glUniform1i(glGetUniformLocation(Program,"myTextureSampler"), 0);

	glBindVertexArray(VAO);
	glDrawElements(GL_QUADS, indices.size(), GL_UNSIGNED_SHORT, 0);
	//GL_QUADS   GL_TRIANGLES
	glDisableVertexAttribArray(0);

	glUseProgram(0);

	glFlush();

	checkOpenGLerror();

	glutSwapBuffers();
}

/*void keyboard(unsigned char key, int x, int y)
{
	switch (key)
	{
	case 'w':
		rotateX += 10;
		break;
	case 's':
		rotateX -= 10;
		break;
	case 'a':
		rotateY -= 10;
		break;
	case 'd':
		rotateY += 10;
		break;
	default:
		break;
	}

	glutPostRedisplay();
}

void specialKeys(int key, int x, int y) {
	switch ((int)key) {
	case GLUT_KEY_UP: rotate_x += 5; break;
	case GLUT_KEY_DOWN: rotate_x -= 5; break;
	case GLUT_KEY_RIGHT: rotate_y += 5; break;
	case GLUT_KEY_LEFT: rotate_y -= 5; break;
	case GLUT_KEY_PAGE_UP: rotate_z += 5; break;
	case GLUT_KEY_PAGE_DOWN: rotate_z -= 5; break;
	case GLUT_KEY_F1: rotate_x = rotate_y = rotate_z = mode = 0; break;
	case GLUT_KEY_F2: rotate_x = rotate_y = rotate_z = 0; mode = 1; break;
	case GLUT_KEY_F3: rotate_x = rotate_y = rotate_z = 0; mode = 2; break;
	case GLUT_KEY_SHIFT_L: rotate_x = rotate_y = rotate_z = 0; projection = 0; break;
	case GLUT_KEY_CTRL_L: rotate_x = rotate_y = rotate_z = 0; projection = 1; break;
	}
	glutPostRedisplay();
}
*/

int main(int argc, char **argv) {
	glutInit(&argc, argv);
	glutInitDisplayMode(GLUT_DEPTH | GLUT_RGBA | GLUT_ALPHA | GLUT_DOUBLE);
	glutInitWindowSize(1000, 800);
	glutCreateWindow("Simple shaders");
	GLenum glew_status = glewInit();
	glClearColor(0, 0, 0, 0);
	glEnable(GL_DEPTH_TEST);
	glDepthFunc(GL_LESS);

	initShader();

	_LoadImage();

	initVBO();

	glutReshapeFunc(resizeWindow);
	glutDisplayFunc(render1);
	//glutKeyboardFunc(keyboard);
	glutMainLoop();

	freeShader();
}