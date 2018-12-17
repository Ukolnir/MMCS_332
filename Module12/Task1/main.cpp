#define _CRT_SECURE_NO_WARNINGS
#include <iostream>
#include <Windows.h>
//#include "D:\_Downloads\MMCS_332\glew-2.1.0\include\GL\glew.h"
//#include "D:\_Downloads\MMCS_332\glew-2.1.0\include\GL\wglew.h"
//#include "D:\_Downloads\MMCS_332\Module10\lib\freeglut.h"
//#include "D:\_Downloads\MMCS_332\Module10\lib\freeglut_std.h"
//#include "D:\_Downloads\MMCS_332\Module10\lib\freeglut_ext.h"
#include "D:\Документы\OneDrive\Документы\7 семестр\комп. графика\MMCS_332\glew-2.1.0\include\GL\glew.h"
#include "D:\Документы\OneDrive\Документы\7 семестр\комп. графика\MMCS_332\glew-2.1.0\include\GL\wglew.h"
#include "D:\Документы\OneDrive\Документы\7 семестр\комп. графика\MMCS_332\Module10\lib\freeglut.h"
#include "D:\Документы\OneDrive\Документы\7 семестр\комп. графика\MMCS_332\Module10\lib\freeglut_std.h"
#include "D:\Документы\OneDrive\Документы\7 семестр\комп. графика\MMCS_332\Module10\lib\freeglut_ext.h"
#include <string>
#include <vector>
#include <iterator>
#include <fstream>
#include <sstream>

using namespace std;

vector<GLfloat> vertices;
vector<GLushort> elements;
int w, h;
GLuint VBO, VBI, Program, VAO;
GLint Attribute_vertex, UnifColor;

void initGL()
{
	glClearColor(0, 0, 0, 0);
}

void checkOpenGLerror()
{
	GLenum errCode;
	if ((errCode = glGetError()) != GL_NO_ERROR)
		std::cout << "OpenGl error! - " << gluErrorString(errCode);
}

void ShaderLog(unsigned int shader)
{
	int info = 0;
	int charWritten = 0;
	char * infolog;

	glGetShaderiv(shader, GL_INFO_LOG_LENGTH, &info);

	if (info > 1)
	{
		infolog = new char[info];
		if (infolog == NULL)
		{
			std::cout << "ERROR: Could not allocate InfoLog buffer\n";
			exit(1);
		}
		glGetShaderInfoLog(shader, info, &charWritten, infolog);
		std::cout << "InfoLog: " << infolog << "\n\n\n";
		delete[] infolog;
	}
}

void resizeWindow(int width, int height)
{
	glViewport(0, 0, width, height);
}

void InitShader()
{
	const char * VsSource = "attribute vec3 coord;\n"
		"in vec3 vertexColor; \n"
		"out vec3 fragmentColor; \n"
		"void main() {\n"
		"gl_Position = vec4(coord, 1); \n"
		"fragmentColor = vertexColor; \n"
		"}\n";

	const char * fsSource = "uniform vec4 color_front; \n"

		"void main() { \n"
		"gl_FragColor = color_front; \n"
		"} \n";

	GLuint vShader;
	vShader = glCreateShader(GL_VERTEX_SHADER);
	glShaderSource(vShader, 1, &VsSource, NULL);
	glCompileShader(vShader);

	std::cout << "vertex shader \n";
	ShaderLog(vShader);

	Program = glCreateProgram();
	glAttachShader(Program, vShader);

	glLinkProgram(Program);
	int link_ok;
	glGetProgramiv(Program, GL_LINK_STATUS, &link_ok);
	if (!link_ok)
	{
		std::cout << "error attach shaders \n";
		return;
	}

	const char* attr_name = "coord";
	Attribute_vertex = glGetAttribLocation(Program, attr_name);
	if (Attribute_vertex == -1)
	{
		std::cout << "could not bind attrib " << attr_name << std::endl;
		return;
	}
	checkOpenGLerror();
}


void createBuffers()
{
	glGenBuffers(1, &VBO);
	glBindBuffer(GL_ARRAY_BUFFER, VBO);

	glBufferData(GL_ARRAY_BUFFER,
		vertices.size() * sizeof(GLfloat),
		vertices.data(),
		GL_STATIC_DRAW);
	

	glGenBuffers(1, &VBI);
	glBindBuffer(GL_ELEMENT_ARRAY_BUFFER, VBI);
	glBufferData(GL_ELEMENT_ARRAY_BUFFER,
		elements.size() * sizeof(GLushort),
		elements.data(),
		GL_STATIC_DRAW);
	
	glVertexAttribPointer(0, 3, GL_FLOAT, GL_FALSE, 3 * sizeof(float), (void*)0);
	glEnableVertexAttribArray(0);

	glClear(GL_COLOR_BUFFER_BIT);
	glUseProgram(Program);
	glBindVertexArray(VAO);
	glDrawElements(GL_TRIANGLES, elements.size() / 3, GL_UNSIGNED_INT, 0);
	glBindVertexArray(0);
	checkOpenGLerror();
}

void freeShader()
{
	glUseProgram(0);
	glDeleteProgram(Program);
}

void freeBuffers()
{
	glBindBuffer(GL_ARRAY_BUFFER, 0);
	glDeleteBuffers(1, &VBO);

	glBindBuffer(GL_ELEMENT_ARRAY_BUFFER, 0);
	glDeleteBuffers(1, &VBI);
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
	loadOBJ("D:\\Документы\\OneDrive\\Документы\\7 семестр\\комп. графика\\MMCS_332\\Module12\\Task1\\vase.obj");
	createBuffers();
}

int main(int argc, char **argv)
{
	glutInit(&argc, argv);
	glutInitDisplayMode(GLUT_RGBA | GLUT_ALPHA | GLUT_DOUBLE);
	glutInitWindowSize(800, 600);
	glutCreateWindow("obj model");

	GLenum glew_status = glewInit();
	if (GLEW_OK != glew_status)
	{
		std::cout << "Error: " << glewGetErrorString(glew_status) << "\n";
		return 1;
	}

	if (!GLEW_VERSION_2_0)
	{
		std::cout << "No support for OpenGL 2.0 found\n";
		return 1;
	}

	initGL();
	InitShader();
	createBuffers();

	glutReshapeFunc(resizeWindow);
	glutDisplayFunc(createBuffers);
	glutMainLoop();

	freeShader();
}
