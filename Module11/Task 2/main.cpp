#define _CRT_SECURE_NO_WARNINGS
#include <iostream>
#include <Windows.h>
#include "glew.h"
#include "wglew.h"
#include "freeglut.h"
#include <string>
#include <vector>
#include <fstream>
using namespace std;

//-----------------------------COLORS
static float frag_color[3] = { 160.0f, 32.0f, 240.0f };
static float bfrag_color[3] = {173.0f, 255.0f, 47.0f };

//-----------------------------MODE FILL
//----------------------------- 0 - full, 1 - rows, 2 - columns
char mode = 2;

//-----------------------------Width line

int cntLine = 100;

//-------------------------------------------------------------------

string vsPath = "C:\\Users\\Ёллоне\\Desktop\\Task 2\\vertex.shader";
string fsPath1 = "C:\\Users\\Ёллоне\\Desktop\\Task 2\\fragment1.shader";
string fsPath2 = "C:\\Users\\Ёллоне\\Desktop\\Task 2\\fragment_rows.shader";
string fsPath3 = "C:\\Users\\Ёллоне\\Desktop\\Task 2\\fragment_columns.shader";

int w, h;
GLuint Program;
GLint  Unif_matr;
GLint  Attrib_vertex;
GLint  Unif_color;
GLint  Unif_color_back;
GLint  Unif_cntLine;
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

	if(!mode)
		_f = loadFile(fsPath1);
	else if(mode == 1)
		_f = loadFile(fsPath2);
	else
		_f = loadFile(fsPath3);
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

	const char* unif_color = "color_front";
	Unif_color = glGetUniformLocation(Program, unif_color);
	if (Unif_color == -1) { std::cout << "could not bind uniform " << unif_color << std::endl;   return; }

	if (mode) {
		const char* cb = "color_back";
		Unif_color_back = glGetUniformLocation(Program, cb);
		if (Unif_color_back == -1) { std::cout << "could not bind uniform " << cb << std::endl;   return; }

		const char* cnt = "cnt";
		Unif_cntLine = glGetUniformLocation(Program, cnt);
		if (Unif_cntLine == -1) { std::cout << "could not bind uniform " << cnt << std::endl;   return; }
	}
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

	static float cf[4] = { frag_color[0]/255.0f , frag_color[1] / 255.0f, frag_color[2] / 255.0f, 1.0f };
	glUniform4fv(Unif_color, 1, cf);

	if (mode) {
		static float cb[4] = { bfrag_color[0] / 255.0f , bfrag_color[1] / 255.0f, bfrag_color[2] / 255.0f, 1.0f };
		glUniform4fv(Unif_color_back, 1, cb);

		glUniform1i(Unif_cntLine, cntLine);
	}

	//glBegin(GL_QUADS);
	glBegin(GL_TRIANGLES);
	glColor3f(1.0, 0.0, 0.0); glVertex2f(-0.5f, -0.5f); 
	glColor3f(0.0, 1.0, 0.0); glVertex2f(-0.5f, 0.5f);  
	glColor3f(0.0, 0.0, 1.0); glVertex2f(0.5f, 0.5f);  
	//glColor3f(1.0, 1.0, 1.0); glVertex2f(0.5f, -0.5f);  
	glEnd();

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
	glClearColor(0, 0, 0, 0);

	GLenum glew_status = glewInit();  

	initShader();

	glutReshapeFunc(resizeWindow);  
	glutDisplayFunc(render2);
	glutMainLoop();

	freeShader(); 
}