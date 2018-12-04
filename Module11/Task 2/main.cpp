#include <GL\glew.h>
#include <GL\freeglut.h>
#include <string>
#include <vector>

using std::string;

static int w = 800, h = 800;

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

void loadOBJ(string path) {

	std::vector< unsigned int > vertexIndices, uvIndices, normalIndices;
	std::vector< vec3 > temp_vertices;
	std::vector< glm::vec3 > temp_uvs;
	std::vector< glm::vec3 > temp_normals;

	FILE * file = fopen(path.c_str, "r");
	if (file == NULL) {
		printf("Impossible to open the file !\n");
		return;
	}

	while (1) {
		char lineHeader[128];
		// читаем первый символ в файле
		int res = fscanf(file, "%s", lineHeader);
		if (res == EOF)
			break; // EOF = Конец файла. Заканчиваем цикл чтения
				   // else : парсим строку
	}
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