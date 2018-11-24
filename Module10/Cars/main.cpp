#define _CRT_SECURE_NO_WARNINGS

#include <iostream>
#include "D:\Документы\OneDrive\Документы\7 семестр\комп. графика\freeglut\include\GL\freeglut.h"
#include "D:\Документы\OneDrive\Документы\7 семестр\комп. графика\freeglut\include\GL\SOIL.h"

unsigned int texture;
unsigned int photo_tex;
int w, h;

void LoadImage() {

}


void Init(void) {
	glClearColor(0.0f, 0.0f, 0.0f, 1.0f);
}

void Update(void) {
	//LoadImage();
	double angle = 1;
	angle += 0.5f;
	glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);

	gluLookAt(100.0f, 100.0f, 100.0f, 60.0f, 50.0f, 40.0f, 0.0f, 1.0f, 0.0f);
	glRotatef(angle, 0.0f, 1.0f, 0.0f);

	texture = SOIL_load_OGL_texture("road.bmp", SOIL_LOAD_AUTO, SOIL_CREATE_NEW_ID, SOIL_FLAG_INVERT_Y);
	glGenTextures(1, &texture);
	glBindTexture(GL_TEXTURE_2D, texture);
	
	glBegin(GL_POLYGON);
	glTexCoord2d(0, 0); glVertex3d(0, 0, 0);
	glTexCoord2d(1, 0); glVertex3d(320, 0, 0);
	glTexCoord2d(1, 1); glVertex3d(320, 0, 320);
	glTexCoord2d(0, 1); glVertex3d(0, 0, 320);
	glEnd();
	glFlush();
	glDisable(GL_TEXTURE_2D);
}
//Функця вызываемая при изменении размеров окна
void Reshape(int width, int height) {
	w = width;
	h = height;
	glViewport(0, 0, w, h);
	glMatrixMode(GL_PROJECTION);
	glLoadIdentity();
	gluPerspective(65.0f, w / h, 1.0f, 1000.0f);
	glMatrixMode(GL_MODELVIEW);

}

int main(int argc, char ** argv) {
	glutInit(&argc, argv);
	glutInitWindowPosition(100, 100);
	glutInitWindowSize(800, 600);
	glutInitDisplayMode(GLUT_SINGLE);

	glutCreateWindow("Cars");

	//glutIdleFunc(Update);
	glutDisplayFunc(Update);
	glutReshapeFunc(Reshape);
	Update();
	glutMainLoop();
	return 0;
}