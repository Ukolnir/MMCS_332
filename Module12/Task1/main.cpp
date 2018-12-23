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
#include "D:\Документы\OneDrive\Документы\7 семестр\комп. графика\MMCS_332\glm\glm\gtc\matrix_transform.hpp"
#include <iterator>
#include <sstream>

using namespace std;
#include "GlShader.h";

string vsPath = "D:\\Документы\\OneDrive\\Документы\\7 семестр\\комп. графика\\MMCS_332\\Module12\\Task1\\vertex.shader";
string fsPath = "D:\\Документы\\OneDrive\\Документы\\7 семестр\\комп. графика\\MMCS_332\\Module12\\Task1\\fragment.shader";

int w, h;
// Наш класс шейдера
GlShader shader;
//! Переменные с индентификаторами ID
//! ID атрибута вершин
GLint  Attrib_vertex;
//! ID атрибута цветов
GLint  Attrib_color;
//! ID юниформ матрицы проекции
GLint  Unif_matrix;
//! ID Vertex Buffer Object
GLuint VBO_vertex;
//! ID Vertex Buffer Object
GLuint VBO_color;
//! ID VBO for element indices
GLuint VBO_element;
//! Количество индексов
GLint Indices_count;
//! Матрица проекции
mat4 Matrix_projection;
float x, y, z, model_angle, is_ahead, is_back;


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
	x = -80; y = 0; z = -350;
	model_angle = 0;
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
	//! Исходный код шейдеров
	shader.loadFiles(vsPath, fsPath);

	if (!shader.isLoad())
	{
		std::cout << "error load shader \n";
		return;
	}

	///! Вытягиваем ID атрибута из собранной программы 
	Attrib_vertex = shader.getAttribLocation("coord");

	//! Вытягиваем ID юниформ
	Attrib_color = shader.getAttribLocation("color");

	//! Вытягиваем ID юниформ матрицы проекции
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

//! Инициализация VBO_vertex
void initVBO()
{
	//! Вершины куба
	normalobj();
	// Создаем буфер для вершин
	glGenBuffers(1, &VBO_vertex);
	glBindBuffer(GL_ARRAY_BUFFER, VBO_vertex);
	glBufferData(GL_ARRAY_BUFFER, sizeof(vertex) * vert.size()/3, vertices, GL_STATIC_DRAW);

	// Создаем буфер для цветов вершин
	glGenBuffers(1, &VBO_color);
	glBindBuffer(GL_ARRAY_BUFFER, VBO_color);
	glBufferData(GL_ARRAY_BUFFER, sizeof(vertex) * vert.size() / 3, colors, GL_STATIC_DRAW);

	// Создаем буфер для индексов вершин
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
	w = width; h = height;
}


//! Отрисовка
void render()
{
	glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);

	//! Устанавливаем шейдерную программу текущей
	shader.use();
	//! Передаем матрицу в шейдер
	shader.setUniform(Unif_matrix, Matrix_projection);

	//! Подлючаем буфер с индексами вершин общий для цветов и их вершин
	glBindBuffer(GL_ELEMENT_ARRAY_BUFFER, VBO_element);

	//! ВЕРШИНЫ
	//! Включаем массив атрибутов для вершин
	glEnableVertexAttribArray(Attrib_vertex);
	//! Подключаем VBO
	glBindBuffer(GL_ARRAY_BUFFER, VBO_vertex);
	//! Указывая pointer 0 при подключенном буфере, мы указываем что данные в VBO
	glVertexAttribPointer(Attrib_vertex, 3, GL_FLOAT, GL_FALSE, 0, 0);

	//! ЦВЕТА
	//! Включаем массив атрибутов для цветов
	glEnableVertexAttribArray(Attrib_color);
	glBindBuffer(GL_ARRAY_BUFFER, VBO_color);
	glVertexAttribPointer(Attrib_color, 3, GL_FLOAT, GL_FALSE, 0, 0);


	//! Передаем данные на видеокарту(рисуем)
	glDrawElements(GL_TRIANGLES, Indices_count, GL_UNSIGNED_INT, 0);

	//! Отключаем массив атрибутов
	glDisableVertexAttribArray(Attrib_vertex);

	//! Отключаем массив атрибутов
	glDisableVertexAttribArray(Attrib_color);

	checkOpenGLerror();

	glutSwapBuffers();
}

void draw()
{
	const GLfloat aspectRatio = (GLfloat)w / (GLfloat)h;
	Matrix_projection = glm::perspective(45.0f, aspectRatio, 1.0f, 400.0f);
	// Перемещаем центр нашей оси координат для того чтобы увидеть куб
	Matrix_projection = glm::translate(Matrix_projection, vec3(x, y, z)); // for vase
	//Matrix_projection = glm::translate(Matrix_projection, vec3(-1.0f, 0.0f, -5.0f)); //for cube1
	// Поворачиваем ось координат(тоесть весь мир), чтобы развернуть отрисованное
	Matrix_projection = glm::rotate(Matrix_projection, model_angle, vec3(1.0f, 1.0f, 0.0f));

	render();
}

double gr_cos(float angle) noexcept
{
	return cos(angle / 180 * 3.1415926535);
}

double gr_sin(float angle) noexcept
{
	return sin(angle / 180 * 3.1415926535);
}


void specialKeys(int key, int x1, int y1)
{
	switch (key)
	{
	case GLUT_KEY_ALT_R:
		model_angle += 2;
		break;
	case GLUT_KEY_ALT_L:
		model_angle -= 2;
		break;
	case GLUT_KEY_CTRL_L:
		is_ahead = 1;
		break;
	case GLUT_KEY_CTRL_R:
		is_back = 1;
		break;
	}

	if (is_ahead)
	{
		x += 0.01 * gr_sin(model_angle);
		z += 0.01 * gr_cos(model_angle);
		is_ahead = 0;
	}
	if (is_back)
	{
		x -= 0.01 * gr_sin(model_angle);
		z -= 0.01 * gr_cos(model_angle);
		is_back = 0;
	}

	glutPostRedisplay();
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
	loadOBJ("D:\\Документы\\OneDrive\\Документы\\7 семестр\\комп. графика\\MMCS_332\\Module12\\Task1\\vase.obj");

	initVBO();
	initShader();

	glutReshapeFunc(resizeWindow);
	glutSpecialFunc(specialKeys);
	glutDisplayFunc(draw);
	glutMainLoop();

	//! Освобождение ресурсов, хотя в нашем случаи сюда выполнение никогда не дойдет,
	// так, как управление не выйдет из glutMainLoop цикла
	freeVBO();
}
