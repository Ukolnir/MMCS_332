#define _CRT_SECURE_NO_WARNINGS
#include <iostream>
#include <Windows.h>
#include "glew.h"
#include "wglew.h"
#include "freeglut.h"
#include "SOIL.h"
#include "glm.hpp"
#include <gtc/matrix_transform.hpp>
#include "OBJ_Loader.h"

#define BUFFER_OFFSET(i) ((char *)NULL + (i))

using namespace std;

//-----------------------------SHADER_MODE
//-------------------------- 0 - oneTexture, 1 - mix with Color, 2 - twoTextures
int shader_mode = 0;

//-----------------------------FACTOR MIX
float factor = 0.2f;

//-----------------------------TEXTURES

string texPath1 = "Krushochki.bmp";

//-----------------------------MODEL

string modelPath = "C:\\Users\\Ёллоне\\Desktop\\Task 2\\Dragon.obj";





///----------------------------------------------------------------------------
objl::Loader obj_loader;
GLushort * indeces;
GLfloat * vertexes;
size_t sz;
size_t sz_indces;

GLuint textureID;
void _LoadImage() {
	textureID = SOIL_load_OGL_texture(texPath1.c_str(), SOIL_LOAD_AUTO, SOIL_CREATE_NEW_ID, SOIL_FLAG_MIPMAPS | SOIL_FLAG_INVERT_Y | SOIL_FLAG_NTSC_SAFE_RGB | SOIL_FLAG_COMPRESS_TO_DXT);
}

string vsPath = "C:\\Users\\Ёллоне\\Desktop\\Task 2\\vertex.shader";
string fsPath1 = "C:\\Users\\Ёллоне\\Desktop\\Task 2\\fragment_oneText.shader";

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
glm::mat4 Matrix_projection;
void resizeWindow(int x, int y) {
	if (y == 0 || x == 0) return;

	w = x;
	h = y;
	glViewport(0, 0, w, h);

	Matrix_projection = glm::perspective(80.0f, (float)w / h, 0.01f, 200.0f);
	glm:: vec3 eye = { 1.5,0,0 };
	glm::vec3 center = { 0,0,0 };
	glm::vec3 up = { 0,0,-1 };

	Matrix_projection *= glm::lookAt(eye, center, up);
}

GLuint VBO, EBO;
GLuint colorbuffer;
GLuint texturebuffer;
GLuint VAO;
GLsizei stride, CoordOffset, NormalOffset, TexCoordOffset, ColorOffset;

void _initVAO() {
	obj_loader.LoadFile(modelPath);
	cout << "Loaded" << endl << endl;
	sz = obj_loader.LoadedVertices.size();
	sz_indces = obj_loader.LoadedIndices.size();

	glGenBuffers(1, &VBO);
	glGenBuffers(1, &EBO);
	glGenVertexArrays(1, &VAO);

    auto len = sizeof(GLfloat) * 3 + sizeof(GLfloat) * 3;

	CoordOffset = 0;
	NormalOffset = sizeof(GLfloat) * 3;
	stride = len;

	glBindVertexArray(VAO);
	glBindBuffer(GL_ARRAY_BUFFER, VBO);
	
	GLfloat * data = new GLfloat[len * sz];

	int ind_load = 0;

	for (size_t i = 0; i < sz * len; i += 6) {
		data[i] = obj_loader.LoadedVertices[ind_load].Position.X;
		data[i+1] = obj_loader.LoadedVertices[ind_load].Position.Y;
		data[i+2] = obj_loader.LoadedVertices[ind_load].Position.Z;
		data[i+3] = obj_loader.LoadedVertices[ind_load].Normal.X;
		data[i+4] = obj_loader.LoadedVertices[ind_load].Normal.Y;
		data[i+5] = obj_loader.LoadedVertices[ind_load].Normal.Z;
	}

	glBufferData(GL_ARRAY_BUFFER, len * sz, data, GL_STATIC_DRAW);

	cout << "BufferData VBO" << endl;

	glBindBuffer(GL_ELEMENT_ARRAY_BUFFER, EBO);

	indeces = new GLushort[sz_indces];
	for (size_t i = 0; i < sz_indces; ++i)
		indeces[i] = obj_loader.LoadedIndices[i];

	glBufferData(GL_ELEMENT_ARRAY_BUFFER, sz_indces * sizeof(GLushort), indeces, GL_STATIC_DRAW);

	glBindVertexArray(0);
}

void render() {
	glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);
	glm::mat4 Projection = glm::perspective(glm::radians(45.0f), 4.0f / 3.0f, 0.1f, 200.0f);
	glm::mat4 View = glm::lookAt(
		glm::vec3(4, 3, 3),
		glm::vec3(0, 0, 0), 
		glm::vec3(0, 1, 0) 
	);
	glm::mat4 Model = glm::mat4(1.0f);
	glm::mat4 MVP = Projection * View * Model;

	glUseProgram(Program);

	//GLuint MatrixID = glGetUniformLocation(Program, "MVP");
	//glUniformMatrix4fv(MatrixID, 1, GL_FALSE, &MVP[0][0]);


	glBindVertexArray(VAO);
	glBindBuffer(GL_ARRAY_BUFFER, VBO);
	glEnableVertexAttribArray(0);
	glVertexAttribPointer(0, 3, GL_FLOAT, GL_FALSE, stride, (void*)0);

	glEnableVertexAttribArray(1);
	glVertexAttribPointer(1, 3, GL_FLOAT, GL_FALSE, stride, (void*)(sizeof(GLfloat) * 3));

	glBindBuffer(GL_ELEMENT_ARRAY_BUFFER, EBO);

	glDrawElements(GL_TRIANGLES, sz_indces, GL_UNSIGNED_SHORT, NULL);

	glutSolidTeacup(2);

	glUseProgram(0);

	checkOpenGLerror();
		

	glFlush();

	glutSwapBuffers();
}

int main(int argc, char **argv) {
	glutInit(&argc, argv);
	glutInitDisplayMode(GLUT_DEPTH | GLUT_RGBA | GLUT_ALPHA | GLUT_DOUBLE);
	glutInitWindowSize(1000, 800);
	glutCreateWindow("Simple shaders");
	glClearColor(0, 0, 0, 0);
	glPolygonMode(GL_FRONT_AND_BACK, GL_FILL);
	glEnable(GL_DEPTH_TEST);
	glDepthFunc(GL_LESS);
	GLenum glew_status = glewInit();

	initShader();

	_initVAO();

	glutReshapeFunc(resizeWindow);
	glutDisplayFunc(render);
	glutMainLoop();

	freeShader();
}