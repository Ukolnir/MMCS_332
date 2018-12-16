
#define _CRT_SECURE_NO_WARNINGS

#include <iostream>
#include <Windows.h>
#include "glew.h"
#include "wglew.h"
#include "freeglut.h"
#include <string>
#include <vector>
#include <algorithm>
#include <functional>
#include <iterator>
#include <fstream>
#include <sstream>
#include <mat3x3.hpp>
#include <trigonometric.hpp>
using namespace std;

//----------------------------ANGLE
float angle = 10.0f;
//----------------------------SCALE ’
float scale_x = 0.9f;
//----------------------------SCALE Y
float scale_y = 0.5f;

//----------------------------MATRIX TRANSFORM
//----------------------------  == 0 - scale, != 0 - rotate
char mode = 0;






float rotate_z = 1.0f;

//-------------------------------------------------------------------
int w, h;

GLuint Program;
GLint  Unif_matr;
GLint  Attrib_vertex;
GLint  Unif_color;
string vsPath = "C:\\Users\\Ёллоне\\Desktop\\Task 1\\vertex.shader";
string fsPath = "C:\\Users\\Ёллоне\\Desktop\\Task 1\\fragment.shader";

void checkOpenGLerror()
{
	GLenum errCode;
	if ((errCode = glGetError()) != GL_NO_ERROR)
		std::cout << "OpenGl error! - " << gluErrorString(errCode);
}

string loadFile(string path) {
	ifstream fs(path, ios::in);
	if (!fs) cerr << "Cannot open " << path << endl;
	string fsS;
	while (getline(fs, fsS, '\0')) 
		cout << fsS << endl;

	return fsS;
}

void initShader() {  
	string _f = loadFile(vsPath);
	const char* vsSource = _f.c_str();

	GLuint vShader, fShader;

	vShader = glCreateShader(GL_VERTEX_SHADER);
	glShaderSource(vShader, 1, &vsSource, NULL);
	glCompileShader(vShader);

	_f = loadFile(fsPath);
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
	if (!link_ok)  {   std::cout << "error attach shaders \n";   return;  } 


	const char* attr_name = "coord";  
	Attrib_vertex = glGetAttribLocation(Program, attr_name);  
	if (Attrib_vertex == -1) { cout << "could not bind attrib " << attr_name << endl;   return; }

	const char* unif_name = "matrix";  
	Unif_matr = glGetUniformLocation(Program, unif_name);  
	if (Unif_matr == -1)  {   std::cout << "could not bind uniform " << unif_name << std::endl;   return;  } 

	const char* unif_color = "color";
	Unif_color = glGetUniformLocation(Program, unif_color);
	if (Unif_color == -1) { std::cout << "could not bind uniform " << unif_color << std::endl;   return; }

	checkOpenGLerror();
} 

void freeShader() {  
	glUseProgram(0);  
	glDeleteProgram(Program); 
} 

void resizeWindow(int width, int height) { glViewport(0, 0, width, height); }

void render2() {

	glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);    
	glLoadIdentity();  

	glUseProgram(Program); 

	static glm::mat4 scale = {  scale_x, 0.0f, 0.0f, 0.0f, 
								0.0f, scale_y, 0.0f, 0.0f, 
								0.0f, 0.0f, 1.0f, 0.0f, 
								0.0f, 0.0f, 0.0f, 1.0f};

	float a = angle * 3.14f / 180.0f;

	static glm::mat4 rotate = { 1.0f, 0.0f, 0.0f, 0.0f, 
								0.0f, glm::cos(a), -glm::sin(a), 0.0f, 
								0.0f, glm::sin(a), glm::cos(a), 0.0f, 
								0.0f, 0.0f, 0.0f, 1.0f };

	if(mode)
		glUniformMatrix3fv(Unif_matr, 1, GL_FALSE, &rotate[0][0]);
	else
		glUniformMatrix4fv(Unif_matr, 1, GL_FALSE, &scale[0][0]);

	static float red[4] = { 1.0f, 0.0f, 0.0f, 1.0f };
	glUniform4fv(Unif_color, 1, red);

	glutSolidCube(1);

	/*glBegin(GL_QUADS);  
	glColor3f(1.0, 0.0, 0.0); glVertex2f(-0.5f, -0.5f); 
	glColor3f(0.0, 1.0, 0.0); glVertex2f(-0.5f, 0.5f);  
	glColor3f(0.0, 0.0, 1.0); glVertex2f(0.5f, 0.5f);  
	glColor3f(1.0, 1.0, 1.0); glVertex2f(0.5f, -0.5f);  
	glEnd();*/

	glFlush();

	glUseProgram(0); 

	checkOpenGLerror();

	glutSwapBuffers();
}

int main(int argc, char **argv) {
	glutInit(&argc, argv);  
	glutInitDisplayMode(GLUT_DEPTH | GLUT_RGBA | GLUT_ALPHA | GLUT_DOUBLE);  
	glutInitWindowSize(800, 800);  
	glutCreateWindow("Simple shaders");  
	glEnable(GL_DEPTH_TEST);
	glClearColor(0, 0, 1, 0);

	GLenum glew_status = glewInit();  
	if (GLEW_OK != glew_status)  {   
		std::cout << "Error: " << glewGetErrorString(glew_status) << "\n";   return 1;  } 

	if (!GLEW_VERSION_2_0)  { std::cout << "No support for OpenGL 2.0 found\n";   return 1;  } 

	initShader();

	glutReshapeFunc(resizeWindow);  
	glutDisplayFunc(render2);
	glutMainLoop();

	freeShader(); 
}