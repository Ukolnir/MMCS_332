#define _CRT_SECURE_NO_WARNINGS
#include <iostream>
#include <Windows.h>
#include "D:\���������\OneDrive\���������\7 �������\����. �������\MMCS_332\glew-2.1.0\include\GL\glew.h"
#include "D:\���������\OneDrive\���������\7 �������\����. �������\MMCS_332\glew-2.1.0\include\GL\wglew.h"
#include "D:\���������\OneDrive\���������\7 �������\����. �������\MMCS_332\Module10\lib\freeglut.h"
#include "D:\���������\OneDrive\���������\7 �������\����. �������\MMCS_332\Module10\lib\freeglut_std.h"
#include "D:\���������\OneDrive\���������\7 �������\����. �������\MMCS_332\Module10\lib\freeglut_ext.h"
#include <string>
#include <vector>
#include <fstream>
#include "D:\���������\OneDrive\���������\7 �������\����. �������\MMCS_332\glm\glm\glm.hpp"
#include "D:\���������\OneDrive\���������\7 �������\����. �������\MMCS_332\glm\glm\gtc\matrix_transform.hpp"
#include <iterator>
#include <sstream>

using namespace std;
#include "GlShader.h";

// ��� ����� �������
GlShader shader;
//! ���������� � ����������������� ID
//! ID �������� ������
GLint  Attrib_vertex;
//! ID �������� ������
GLint  Attrib_color;
//! ID ������� ������� ��������
GLint  Unif_matrix;
//! ID Vertex Buffer Object
GLuint VBO_vertex;
//! ID Vertex Buffer Object
GLuint VBO_color;
//! ID VBO for element indices
GLuint VBO_element;
//! ���������� ��������
GLint Indices_count;
//! ������� ��������
mat4 Matrix_projection;

//! �������
struct vertex
{
	GLfloat x;
	GLfloat y;
	GLfloat z;
};

//! ������������� OpenGL, ����� ���� �� ������������
void initGL()
{
	glClearColor(0, 0, 0, 0);
	glEnable(GL_DEPTH_TEST);
}

//! �������� ������ OpenGL, ���� ���� �� ������� � ������� ��� ������
void checkOpenGLerror()
{
	GLenum errCode;
	if ((errCode = glGetError()) != GL_NO_ERROR)
		std::cout << "OpenGl error! - " << gluErrorString(errCode);
}

//! ������������� ��������
void initShader()
{
	//! �������� ��� ��������
	const GLchar* vsSource =
		"attribute vec3 coord;\n"
		"attribute vec3 color;\n"
		"varying vec3 var_color;\n"
		"uniform mat4 matrix;\n"
		"void main() {\n"
		"  gl_Position = matrix * vec4(coord, 1.0);\n"
		"  var_color = color;\n"
		"}\n";
	const GLchar* fsSource =
		"varying vec3 var_color;\n"
		"void main() {\n"
		"  gl_FragColor = vec4(var_color, 1.0);\n"
		"}\n";

	if (!shader.load(vsSource, fsSource))
	{
		std::cout << "error load shader \n";
		return;
	}

	///! ���������� ID �������� �� ��������� ��������� 
	Attrib_vertex = shader.getAttribLocation("coord");

	//! ���������� ID �������
	Attrib_color = shader.getAttribLocation("color");

	//! ���������� ID ������� ������� ��������
	Unif_matrix = shader.getUniformLocation("matrix");

	checkOpenGLerror();
}

vector<GLfloat> vert;
vector<GLint> elements;
vertex * vertices, * colors;
GLint * indices;

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
			s >> v; vert.push_back(v);
			s >> v; vert.push_back(v);
			s >> v; vert.push_back(v);
		}
		else if (line.substr(0, 2) == "f ")
		{
			istringstream s(line.substr(2));
			GLint a, b, c;
			s >> a; s >> b; s >> c;
			elements.push_back(a); elements.push_back(b); elements.push_back(c);
		}
	}
}

void normalobj()
{
	vertices = new vertex[vert.size() / 3];
	colors = new vertex[vert.size() / 3];
	indices = new GLint[elements.size()];

	for (int i = 0; i < vert.size() / 3; ++i)
	{
		vertex v = { vert[i * 3], vert[i * 3 + 1], vert[i * 3 + 2] };
		vertices[i] = v;
	}

	for (int i = 0; i < vert.size() / 3; ++i)
	{
		vertex v = { rand() % 255 / 255.0, rand() % 255 / 255.0, rand() % 255 / 255.0 };
		colors[i] = v;
	}
	for (int i = 0; i < elements.size(); ++i)
		indices[i] = elements[i];
}

//! ������������� VBO_vertex
void initVBO()
{
	//! ������� ����
	normalobj();

	/*vertex vertices[] = {
		{ -1.0f, -1.0f, -1.0f },
		{ 1.0f, -1.0f, -1.0f },
		{ 1.0f,  1.0f, -1.0f },
		{ -1.0f, 1.0f, -1.0f },
		{ -1.0f, -1.0f,  1.0f },
		{ 1.0f, -1.0f,  1.0f },
		{ 1.0f,  1.0f,  1.0f },
		{ -1.0f,  1.0f,  1.0f }
	};
	//! ����� ���� ��� ����� ����������
	vertex colors[] = {
		{ 1.0f, 0.5f, 1.0f },
		{ 1.0f, 0.5f, 0.5f },
		{ 0.5f, 0.5f, 1.0f },
		{ 0.0f, 1.0f, 1.0f },
		{ 1.0f, 0.0f, 1.0f },
		{ 1.0f, 1.0f, 0.0f },
		{ 1.0f, 0.0f, 1.0f },
		{ 0.0f, 1.0f, 1.0f }
	};
	//! ������� ������, ����� � ��� ������
	GLint indices[] = {
		0, 4, 5, 0, 5, 1,
		1, 5, 6, 1, 6, 2,
		2, 6, 7, 2, 7, 3,
		3, 7, 4, 3, 4, 0,
		4, 7, 6, 4, 6, 5,
		3, 0, 1, 3, 1, 2
	};*/

	// ������� ����� ��� ������
	glGenBuffers(1, &VBO_vertex);
	glBindBuffer(GL_ARRAY_BUFFER, VBO_vertex);
	glBufferData(GL_ARRAY_BUFFER, sizeof(vertex) * vert.size()/3, vertices, GL_STATIC_DRAW);

	// ������� ����� ��� ������ ������
	glGenBuffers(1, &VBO_color);
	glBindBuffer(GL_ARRAY_BUFFER, VBO_color);
	glBufferData(GL_ARRAY_BUFFER, sizeof(vertex) * vert.size() / 3, colors, GL_STATIC_DRAW);

	// ������� ����� ��� �������� ������
	glGenBuffers(1, &VBO_element);
	glBindBuffer(GL_ELEMENT_ARRAY_BUFFER, VBO_element);
	glBufferData(GL_ELEMENT_ARRAY_BUFFER, sizeof(GLint) * elements.size(), indices, GL_STATIC_DRAW);

	Indices_count = elements.size(); //sizeof(indices) / sizeof(indices[0]);

	checkOpenGLerror();
}

void freeVBO()
{
	glBindBuffer(GL_ARRAY_BUFFER, 0);
	glBindBuffer(GL_ELEMENT_ARRAY_BUFFER, 0);

	glDeleteBuffers(1, &VBO_element);
	glDeleteBuffers(1, &VBO_element);
	glDeleteBuffers(1, &VBO_color);
}

void resizeWindow(int width, int height)
{
	glViewport(0, 0, width, height);

	height = height > 0 ? height : 1;
	const GLfloat aspectRatio = (GLfloat)width / (GLfloat)height;

	Matrix_projection = glm::perspective(45.0f, aspectRatio, 1.0f, 200.0f);
	// ���������� ����� ����� ��� ��������� ��� ���� ����� ������� ���
	Matrix_projection = glm::translate(Matrix_projection, vec3(-40.0f, 20.0f, -220.0f));
	// ������������ ��� ���������(������ ���� ���), ����� ���������� ������������
	Matrix_projection = glm::rotate(Matrix_projection, 0.0f, vec3(1.0f, 1.0f, 0.0f));
}

//! ���������
void render()
{
	glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);

	//! ������������� ��������� ��������� �������
	shader.use();
	//! �������� ������� � ������
	shader.setUniform(Unif_matrix, Matrix_projection);

	//! ��������� ����� � ��������� ������ ����� ��� ������ � �� ������
	glBindBuffer(GL_ELEMENT_ARRAY_BUFFER, VBO_element);

	//! �������
	//! �������� ������ ��������� ��� ������
	glEnableVertexAttribArray(Attrib_vertex);
	//! ���������� VBO
	glBindBuffer(GL_ARRAY_BUFFER, VBO_vertex);
	//! �������� pointer 0 ��� ������������ ������, �� ��������� ��� ������ � VBO
	glVertexAttribPointer(Attrib_vertex, 3, GL_FLOAT, GL_FALSE, 0, 0);

	//! �����
	//! �������� ������ ��������� ��� ������
	glEnableVertexAttribArray(Attrib_color);
	glBindBuffer(GL_ARRAY_BUFFER, VBO_color);
	glVertexAttribPointer(Attrib_color, 3, GL_FLOAT, GL_FALSE, 0, 0);


	//! �������� ������ �� ����������(������)
	glDrawElements(GL_TRIANGLES, Indices_count, GL_UNSIGNED_INT, 0);

	//! ��������� ������ ���������
	glDisableVertexAttribArray(Attrib_vertex);

	//! ��������� ������ ���������
	glDisableVertexAttribArray(Attrib_color);

	checkOpenGLerror();

	glutSwapBuffers();
}

int main(int argc, char **argv)
{
	glutInit(&argc, argv);
	glutInitDisplayMode(GLUT_RGBA | GLUT_ALPHA | GLUT_DOUBLE);
	glutInitWindowSize(800, 600);
	glutCreateWindow("Simple shaders");

	//! ����������� ����� ������������� ��������
	GLenum glew_status = glewInit();
	if (GLEW_OK != glew_status)
	{
		//! GLEW �� ���������������������
		std::cout << "Error: " << glewGetErrorString(glew_status) << "\n";
		return 1;
	}

	//! ��������� ����������� OpenGL 2.0
	if (!GLEW_VERSION_2_0)
	{
		//! OpenGl 2.0 ��������� �� ��������
		std::cout << "No support for OpenGL 2.0 found\n";
		return 1;
	}

	//! �������������
	initGL();
	loadOBJ("D:\\���������\\OneDrive\\���������\\7 �������\\����. �������\\MMCS_332\\Module12\\Task1\\vase.obj");
	initVBO();
	initShader();

	glutReshapeFunc(resizeWindow);
	glutDisplayFunc(render);
	glutMainLoop();

	//! ������������ ��������, ���� � ����� ������ ���� ���������� ������� �� ������,
	// ���, ��� ���������� �� ������ �� glutMainLoop �����
	freeVBO();
}
