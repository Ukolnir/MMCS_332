#define _CRT_SECURE_NO_WARNINGS
#include <iostream>
#include <Windows.h>
#include "D:\Документы\OneDrive\Документы\7 семестр\комп. графика\MMCS_332\glew-2.1.0\include\GL\glew.h"
#include "D:\Документы\OneDrive\Документы\7 семестр\комп. графика\MMCS_332\glew-2.1.0\include\GL\wglew.h"
#include "D:\Документы\OneDrive\Документы\7 семестр\комп. графика\MMCS_332\Module10\lib\freeglut.h"
#include "D:\Документы\OneDrive\Документы\7 семестр\комп. графика\MMCS_332\Module10\lib\freeglut_std.h"
#include "D:\Документы\OneDrive\Документы\7 семестр\комп. графика\MMCS_332\Module10\lib\freeglut_ext.h"
#include <string>
#include <vector>
#include <fstream>
#include "D:\Документы\OneDrive\Документы\7 семестр\комп. графика\MMCS_332\glm\glm\glm.hpp"
#include <gtc/matrix_transform.hpp>
#include <iterator>
#include <sstream>

using namespace std;

//-----------------------------SHADER_MODE
//-------------------------- 0 - oneTexture, 1 - mix with Color, 2 - twoTextures
int shader_mode = 0;

//-----------------------------FACTOR MIX
float factor = 0.2f;

///----------------------------------------------------------------------------


string vsPath = "C:\\Users\\user\\Desktop\\Task 3\\vertex.shader";
string fsPath1 = "C:\\Users\\user\\Desktop\\Task 3\\fragment_oneText.shader";

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

	//if(!shader_mode)
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
	if (!link_ok) { std::cout << "error attach shaders \n"; }//   return;


	checkOpenGLerror();
}

void freeShader() {
	glUseProgram(0);
	glDeleteProgram(Program);
}

void resizeWindow(int width, int height) { glViewport(0, 0, width, height); }

GLuint vertexbuffer;
GLuint colorbuffer;
GLuint texturebuffer;
GLuint VBI;

vector<GLfloat> vertices;
vector<GLushort> elements;

void loadOBJ(string path)
{
	ifstream in(path, ios::in);
	if (!in)
	{
		cerr << "Cannot open " << path << endl; system("pause");
	}

	string line;
	while (getline(in, line))
	{
		if (line.substr(0, 2) == "v ")
		{
			istringstream s(line.substr(2));
			GLfloat v;
			s >> v; vertices.push_back(v);
			s >> v; vertices.push_back(v);
			s >> v; vertices.push_back(v);
		}
		else if (line.substr(0, 2) == "f ")
		{
			istringstream s(line.substr(2));
			GLushort a, b, c;
			s >> a; s >> b; s >> c;
			elements.push_back(a); elements.push_back(b); elements.push_back(c);
		}
	}
}


void initVBO() {
	GLuint VertexArrayID;
	glGenVertexArrays(1, &VertexArrayID);
	glBindVertexArray(VertexArrayID);

	vector<GLfloat> g_color_buffer_data;

	for (int i = 0; i < vertices.size() / 3; ++i)
	{
		for (int j = 0; j < 3; ++j)
		{
			float c = rand() % 255 / 255.0;
			g_color_buffer_data.push_back(c);
		}
	}
	

	glGenBuffers(1, &vertexbuffer);
	glBindBuffer(GL_ARRAY_BUFFER, vertexbuffer);
	glBufferData(GL_ARRAY_BUFFER, sizeof(GLfloat) * vertices.size(), vertices.data(), GL_STATIC_DRAW);

	glGenBuffers(1, &colorbuffer);
	glBindBuffer(GL_ARRAY_BUFFER, colorbuffer);
	glBufferData(GL_ARRAY_BUFFER, sizeof(GLfloat) * g_color_buffer_data.size(), g_color_buffer_data.data(), GL_STATIC_DRAW);
	
	glGenBuffers(1, &VBI);
	glBindBuffer(GL_ELEMENT_ARRAY_BUFFER, VBI);
	glBufferData(GL_ELEMENT_ARRAY_BUFFER, sizeof(GLfloat) * elements.size(), elements.data(), GL_STATIC_DRAW);
}

void render1() {
	glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);
	glm::mat4 Projection = glm::perspective(glm::radians(45.0f), 4.0f / 3.0f, 0.1f, 100.0f);
	glm::mat4 View = glm::lookAt(
		glm::vec3(-4, -2, -4),
		glm::vec3(0, 0, 0),
		glm::vec3(0, 1, 0)
	);
	glm::mat4 Model = glm::mat4(1.0f);
	glm::mat4 MVP = Projection * View * Model;


	glUseProgram(Program);

	glEnableVertexAttribArray(0);
	glBindBuffer(GL_ARRAY_BUFFER, vertexbuffer);
	glVertexAttribPointer(0, 3, GL_FLOAT, GL_FALSE, 0, (void*)0);

	glEnableVertexAttribArray(1);
	glBindBuffer(GL_ARRAY_BUFFER, colorbuffer);
	glVertexAttribPointer(1, 3, GL_FLOAT, GL_FALSE, 0, (void*)0);

	glBindBuffer(GL_ELEMENT_ARRAY_BUFFER, VBI);

	GLuint MatrixID = glGetUniformLocation(Program, "MVP");
	glUniformMatrix4fv(MatrixID, 1, GL_FALSE, &MVP[0][0]);
	
	glDrawArrays(GL_TRIANGLES, 0, elements.size() / 3);

	glDisableVertexAttribArray(0);

	glFlush();

	glUseProgram(0);

	checkOpenGLerror();

	glutSwapBuffers();
}

int main(int argc, char **argv) {
	glutInit(&argc, argv);
	glutInitDisplayMode(GLUT_DEPTH | GLUT_RGBA | GLUT_ALPHA | GLUT_DOUBLE);
	glutInitWindowSize(1000, 800);
	glutCreateWindow("Simple shaders");
	glClearColor(0, 0, 0, 0);
	glEnable(GL_DEPTH_TEST);
	glDepthFunc(GL_LESS);
	GLenum glew_status = glewInit();

	initShader();
	loadOBJ("C:\\Users\\user\\Desktop\\Task 3\\cube.obj");

	initVBO();

	glutReshapeFunc(resizeWindow);
	glutDisplayFunc(render1);
	glutMainLoop();

	freeShader();
}