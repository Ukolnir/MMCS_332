
#define _CRT_SECURE_NO_WARNINGS

#include <iostream>
#include <Windows.h>
#include "D:\_Downloads\MMCS_332\glew-2.1.0\include\GL\glew.h"
#include "D:\_Downloads\MMCS_332\glew-2.1.0\include\GL\wglew.h"
#include "D:\_Downloads\MMCS_332\Module10\lib\freeglut.h"
#include "D:\_Downloads\MMCS_332\Module10\lib\freeglut_std.h"
#include "D:\_Downloads\MMCS_332\Module10\lib\freeglut_ext.h"
#include "D:\_Downloads\MMCS_332\Module10\lib\SOIL.h"
#include <string>
#include <vector>
#include <algorithm>
#include <functional>
#include <iterator>
#include <fstream>
#include <sstream>

using namespace std;

vector<GLfloat> vertices;
vector<GLushort> elements;
int w, h;
GLuint VBO, VBI;


void checkOpenGLerror()
{
	GLenum errCode;
	if ((errCode = glGetError()) != GL_NO_ERROR)
		std::cout << "OpenGl error! - " << gluErrorString(errCode);
}

void createBuffers()
{
	VBO = 12345;
	VBI = 12345;
	glGenBuffers(1, &VBO);
	glBindBuffer(GL_ARRAY_BUFFER, VBO);

	glBufferData(GL_ARRAY_BUFFER,
		vertices.size() * sizeof(GLfloat),
		vertices.data(),
		GL_STATIC_DRAW);
	glBindBuffer(GL_ARRAY_BUFFER, 0);
	

	glGenBuffers(1, &VBI);
	glBindBuffer(GL_ELEMENT_ARRAY_BUFFER, VBI);
	glBufferData(GL_ELEMENT_ARRAY_BUFFER,
		elements.size() * sizeof(GLushort),
		elements.data(),
		GL_STATIC_DRAW);
	glBindBuffer(GL_ELEMENT_ARRAY_BUFFER, 0);

	checkOpenGLerror();
}


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

void Init(void) {
	loadOBJ("D:\\_Downloads\\MMCS_332\\Module11\\Task1\\x64\\Debug\\vase.obj");
	createBuffers();
}


void DrawVBO()
{
	//GLvoid * p;// = new Glvoid[0];
	glEnableClientState(GL_VERTEX_ARRAY);
	glBindBuffer(GL_ARRAY_BUFFER, VBO);
	glVertexPointer(3, GL_FLOAT, 0, 0);
	glBindBuffer(GL_ELEMENT_ARRAY_BUFFER, VBI);
	glDrawElements(GL_TRIANGLES, elements.size(), GL_UNSIGNED_BYTE, 0);
	glBindBuffer(GL_ELEMENT_ARRAY_BUFFER, 0);
	glDisableClientState(GL_VERTEX_ARRAY);
}

void Update(void) {
	
	//createBuffers();
	//DrawVBO();
	glFlush();
	glutSwapBuffers();


}
//‘ункц€ вызываема€ при изменении размеров окна
void Reshape(int width, int height) {
	w = width;
	h = height;
	glViewport(0, 0, w, h);
	glMatrixMode(GL_PROJECTION);
	glLoadIdentity();
	gluPerspective(65.0f, w / h, 1.0f, 1000.0f);
	glMatrixMode(GL_MODELVIEW);
	glLoadIdentity();
}

int main(int argc, char ** argv) {
	glutInit(&argc, argv);
	glutInitWindowPosition(250, 0);
	glutInitWindowSize(w, h);
	
	glutInitDisplayMode(GLUT_RGBA | GLUT_DOUBLE | GLUT_DEPTH);
	glutCreateWindow("OpenGL");
	glutReshapeFunc(Reshape);
	glutDisplayFunc(DrawVBO);
	glewInit();
	Init();

	glutMainLoop();

//	glutMainLoop();
	return 0;
}