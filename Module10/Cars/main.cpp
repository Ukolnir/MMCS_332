

#include <iostream>
#include <Windows.h>
#include "D:\Документы\OneDrive\Документы\7 семестр\комп. графика\freeglut\include\GL\freeglut.h"
#include "D:\Документы\OneDrive\Документы\7 семестр\комп. графика\freeglut\include\GL\SOIL.h"

GLuint texture, textCar, texWindow;

int w, h, w1, l1;
float p1x, p1z, rotate_x, rotate_y, rotate_z;
bool flags[] = { false, false, false, false, false };

void LoadImage1() {
	texture = SOIL_load_OGL_texture("road.bmp", SOIL_LOAD_AUTO, SOIL_CREATE_NEW_ID, SOIL_FLAG_MIPMAPS | SOIL_FLAG_INVERT_Y | SOIL_FLAG_NTSC_SAFE_RGB | SOIL_FLAG_COMPRESS_TO_DXT);
	textCar = SOIL_load_OGL_texture("D:\\Games\\5.jpg", SOIL_LOAD_AUTO, SOIL_CREATE_NEW_ID, SOIL_FLAG_MIPMAPS | SOIL_FLAG_INVERT_Y | SOIL_FLAG_NTSC_SAFE_RGB | SOIL_FLAG_COMPRESS_TO_DXT);
	texWindow = SOIL_load_OGL_texture("D:\\Games\\window.jpg", SOIL_LOAD_AUTO, SOIL_CREATE_NEW_ID, SOIL_FLAG_MIPMAPS | SOIL_FLAG_INVERT_Y | SOIL_FLAG_NTSC_SAFE_RGB | SOIL_FLAG_COMPRESS_TO_DXT);
}

void Init(void) {
	rotate_x = 0;
	rotate_y = 0;
	rotate_z = 0;
	p1x = -450;
	p1z = 100;
	glClearColor(0.3f, 0.5f, 0.5f, 1.0f);
	glLoadIdentity();
	LoadImage1();

	glLightModelf(GL_LIGHT_MODEL_TWO_SIDE, GL_TRUE);

	glEnable(GL_NORMALIZE);
		
}

void DrawRoad()
{

	glBindTexture(GL_TEXTURE_2D, texture);
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_REPEAT);
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_REPEAT);

	glEnable(GL_TEXTURE_2D);

	glBegin(GL_QUADS);
	glTexCoord2f(0.0, 0.0); glVertex3f(-500.0, 0.0, 0.0);
	glTexCoord2f(0.0, 1.0); glVertex3f(-500.0, 0.0, 320.0);
	glTexCoord2f(1.0, 1.0); glVertex3f(520.0, 0.0, 320.0);
	glTexCoord2f(1.0, 0.0); glVertex3f(520.0, 0.0, 0.0);
	glEnd();
	glDisable(GL_TEXTURE_2D);

}

void DrawCubes()
{
	
	float mat[] = { 1, 0.76, 0.678 };
	float mat1[] = { 0.2, 0.05, 0.2 };
	glColor3f(0.3, 0.1, 0.4);

	glPushMatrix();
	glTranslatef(10.0, 10.0, 10.0);
	glutSolidCube(20);
	glPopMatrix();

	glPushMatrix();
	glTranslatef(10.0, 10.0, 300.0);
	glutSolidCube(20);
	glPopMatrix();

	glPushMatrix();
	glTranslatef(300.0, 10.0, 10.0);
	glutSolidCube(20);
	glPopMatrix();

	glPushMatrix();
	glTranslatef(300.0, 10.0, 300.0);
	glutSolidCube(20);
	glPopMatrix();

	glColor3f(1, 1, 1);

	//рисуем верхние кубы

	glColor3f(1, 0.76, 0.678);
	glPushMatrix();
	glTranslatef(10.0, 25.0, 10.0);
	glutSolidCube(30);
	glPopMatrix();
	
	glPushMatrix();
	glTranslatef(10.0, 25.0, 300.0);
	glutSolidCube(30);
	glPopMatrix();

	glPushMatrix();
	glTranslatef(300.0, 25.0, 10.0);
	glutSolidCube(30);
	glPopMatrix();

	glPushMatrix();
	glTranslatef(300.0, 25.0, 300.0);
	glutSolidCube(30);
	glPopMatrix();

	
	glColor3f(1, 1, 1);

}

void AddLight()
{
	glPushMatrix();
	glLoadIdentity();
	
	GLfloat light_pos[] = { 300.0, 20.0, 10.0, 1.0 };
	GLfloat light_pos1[] = { 10.0, 20.0, 10.0, 1.0 };
	GLfloat light_pos2[] = { 10.0, 20.0, 300.0, 1.0 };
	GLfloat light_pos3[] = { 300.0, 20.0, 300.0, 1.0 };

	GLfloat light_pos0[] = { 360, 360.0, 360.0, 1.0 };

	GLfloat dif[] = { 1, 0.76, 0.978, 1.0 };
	GLfloat dif_p[] = { 0.7, 0.7, 0.2, 1.0 };

	GLfloat sp1[] = { 0, 0, 0 };
	GLfloat coff1[] = { 180 };
	glLightfv(GL_LIGHT0, GL_POSITION, light_pos0);
	glLightfv(GL_LIGHT0, GL_SPOT_DIRECTION, sp1);
	glLightfv(GL_LIGHT0, GL_SPOT_CUTOFF, coff1);

	glLightfv(GL_LIGHT1, GL_POSITION, light_pos);
	glLightfv(GL_LIGHT1, GL_DIFFUSE, dif);


	glLightfv(GL_LIGHT2, GL_POSITION, light_pos1);
	glLightfv(GL_LIGHT2, GL_DIFFUSE, dif);



	glLightfv(GL_LIGHT3, GL_POSITION, light_pos2);
	glLightfv(GL_LIGHT3, GL_DIFFUSE, dif);


	glLightfv(GL_LIGHT4, GL_POSITION, light_pos3);
	glLightfv(GL_LIGHT4, GL_DIFFUSE, dif);
	

	glPushMatrix();
	glTranslatef(p1x, 25, p1z + 7);
	GLfloat light_pos_p[] = { 0, 0, 1 };
	GLfloat sp[] = { 0, 0,  -1 };
	GLfloat coff[] = { 45.0 };
	GLfloat se[] = { 15.0 };

	glLightfv(GL_LIGHT5, GL_POSITION, light_pos_p);
	glLightfv(GL_LIGHT5, GL_SPOT_DIRECTION, sp);
	glLightfv(GL_LIGHT5, GL_SPOT_CUTOFF, coff);
	glLightfv(GL_LIGHT5, GL_SPOT_EXPONENT, se);
	glLightfv(GL_LIGHT5, GL_DIFFUSE, dif_p);
	glPopMatrix();

	glPushMatrix();
	glTranslatef(p1x, 25, p1z + l1 - 7);

	glLightfv(GL_LIGHT6, GL_POSITION, light_pos_p);
	glLightfv(GL_LIGHT6, GL_SPOT_DIRECTION, sp);
	glLightfv(GL_LIGHT6, GL_SPOT_CUTOFF, coff);
	glLightfv(GL_LIGHT6, GL_SPOT_EXPONENT, se);
	glLightfv(GL_LIGHT6, GL_DIFFUSE, dif_p);
	glPopMatrix();
	
	glPopMatrix();
	
}

void DrawCar()
{
	
	int r = 7;
	w1 = 80;
	l1 = 60;
	//рисуем колеса
	glColor3f(0.0, 0.0, 0.0);

	glPushMatrix();
	glTranslatef(p1x + r, r+1, p1z + l1);
	glutSolidTorus(3, r, 54, 54);
	glPopMatrix();

	glPushMatrix();
	glTranslatef(p1x + w1 - r, r+1, p1z + l1);
	glutSolidTorus(3, r, 54, 54);
	glPopMatrix();

	glPushMatrix();
	glTranslatef(p1x + r, r+1, p1z);
	glutSolidTorus(3, r, 54, 54);
	glPopMatrix();

	glPushMatrix();
	glTranslatef(p1x + w1 - r, r+1, p1z);
	glutSolidTorus(3, r, 54, 54);
	glPopMatrix();


	glBindTexture(GL_TEXTURE_2D, textCar);
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_REPEAT);
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_REPEAT);

	glEnable(GL_TEXTURE_2D);


	//рисуем низ
	int d = 12;

	glColor3f(0.40, 0.35, 0.35);

	glBegin(GL_QUAD_STRIP);
	glVertex3f(p1x, 40.0, p1z); glTexCoord2f(0, 0);
	glVertex3f(p1x + w1, 40.0, p1z); glTexCoord2f(1, 0);

	glVertex3f(p1x, 40.0, p1z + l1); glTexCoord2f(0, 1);
	glVertex3f(p1x + w1, 40.0, p1z + l1); glTexCoord2f(1, 1);
	
	glVertex3f(p1x, d, p1z + l1); glTexCoord2f(0, 0);
	glVertex3f(p1x + w1, d, p1z + l1); glTexCoord2f(1, 0);

	glVertex3f(p1x, d, p1z);  glTexCoord2f(1, 1);
	glVertex3f(p1x + w1, d, p1z);  glTexCoord2f(0, 1);

	glVertex3f(p1x, 40.0, p1z);  glTexCoord2f(0, 0);
	glVertex3f(p1x + w1, 40.0, p1z);  glTexCoord2f(1, 0);
	glEnd();


	glBegin(GL_QUADS);
	glVertex3f(p1x, 40.0, p1z);  glTexCoord2f(0, 1);
	glVertex3f(p1x, 40.0, p1z + l1);  glTexCoord2f(1, 1);
	glVertex3f(p1x, d, p1z + l1);  glTexCoord2f(1, 0);
	glVertex3f(p1x, d, p1z);  glTexCoord2f(0, 0);
	glEnd();

	glBegin(GL_QUADS);
	glVertex3f(p1x + w1, 40.0, p1z); glTexCoord2f(0, 1);
	glVertex3f(p1x + w1, 40.0, p1z + l1); glTexCoord2f(1, 1);
	glVertex3f(p1x + w1, d, p1z + l1); glTexCoord2f(1, 0);
	glVertex3f(p1x + w1, d, p1z); glTexCoord2f(0, 0);
	glEnd();
	
	//кабина
	glBegin(GL_QUAD_STRIP);
	glVertex3f(p1x + w1 - 30, 70.0, p1z); glTexCoord2f(0, 0);
	glVertex3f(p1x + w1, 70.0, p1z); glTexCoord2f(1, 0);

	glVertex3f(p1x + w1 - 30, 70.0, p1z + l1); glTexCoord2f(1, 1);
	glVertex3f(p1x + w1, 70.0, p1z + l1); glTexCoord2f(0, 1);

	glVertex3f(p1x + w1 - 30, 40.0, p1z + l1); glTexCoord2f(0, 0);
	glVertex3f(p1x + w1, 40.0, p1z + l1); glTexCoord2f(1, 0);

	glVertex3f(p1x + w1 - 30, 40.0, p1z); glTexCoord2f(0, 1);
	glVertex3f(p1x + w1, 40.0, p1z); glTexCoord2f(1, 1);

	glVertex3f(p1x + w1 - 30, 70.0, p1z); glTexCoord2f(1, 0);
	glVertex3f(p1x + w1, 70.0, p1z); glTexCoord2f(0, 0);
	glEnd();

	glBegin(GL_QUADS);
	glVertex3f(p1x + w1 - 30, 70.0, p1z); glTexCoord2f(0, 0);
	glVertex3f(p1x + w1 - 30, 70.0, p1z + l1); glTexCoord2f(1, 0);
	glVertex3f(p1x + w1 - 30, 40.0, p1z + l1); glTexCoord2f(1, 1);
	glVertex3f(p1x + w1 - 30, 40.0, p1z); glTexCoord2f(0, 1);
	glEnd();
	
	glDisable(GL_TEXTURE_2D);

	glBindTexture(GL_TEXTURE_2D, texWindow);
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_REPEAT);
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_REPEAT);

	glEnable(GL_TEXTURE_2D);
	glColor3f(0.55, 0.9, 0.9);

	glBegin(GL_QUADS);
	glVertex3f(p1x + w1, 70.0, p1z); glTexCoord2f(0, 0);
	glVertex3f(p1x + w1, 70.0, p1z + l1); glTexCoord2f(1, 0);
	glVertex3f(p1x + w1, 40.0, p1z + l1); glTexCoord2f(1, 1);
	glVertex3f(p1x + w1, 40.0, p1z); glTexCoord2f(0, 1);
	glEnd();

	glDisable(GL_TEXTURE_2D);

	//фары
	glPushMatrix();
	glColor3f(0.7, 0.7, 0.2);
	glTranslatef(p1x + w1 + 5, 35, p1z + 10);
	glutSolidSphere(5, 48, 48);
	glPopMatrix();

	glPushMatrix();
	glColor3f(0.7, 0.7, 0.2);
	glTranslatef(p1x + w1 + 5, 35, p1z + l1 - 10);
	glutSolidSphere(5, 48, 48);
	glPopMatrix();

	glColor3f(1, 1, 1);
}

void Update(void) {
	double angle = 1;
	angle += 0.5f;

	glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);
	glLoadIdentity();
	gluLookAt(360.0f, 360.0f, 360.0f, 0.0f, 0.0f, 0.0f, 0.0f, 1.0f, 0.0f);
	glRotatef(rotate_x, 1.0, 0.0, 0.0);
	glRotatef(rotate_y, 0.0, 1.0, 0.0);
	glRotatef(rotate_z, 0.0, 0.0, 1.0);

	glEnable(GL_DEPTH_TEST);
	glEnable(GL_LIGHTING);

	glEnable(GL_LIGHT0);
	if (flags[0])
		glEnable(GL_LIGHT1);
	
	if (flags[1])
		glEnable(GL_LIGHT2);

	if (flags[2])
		glEnable(GL_LIGHT3);

	if (flags[3])
		glEnable(GL_LIGHT4);
	if (flags[4])
	{
		glEnable(GL_LIGHT5);
		glEnable(GL_LIGHT6);
	}
	
	
	AddLight();
	DrawRoad();
	DrawCubes();
	DrawCar();
	
	glDisable(GL_LIGHT0);
	glDisable(GL_LIGHT1);
	glDisable(GL_LIGHT2);
	glDisable(GL_LIGHT3);
	glDisable(GL_LIGHT4);
	glDisable(GL_LIGHT5);
	glDisable(GL_LIGHT6);
	glDisable(GL_LIGHTING);

	glDisable(GL_DEPTH_TEST);
	glFlush();
	glutSwapBuffers();

	
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
	glLoadIdentity();
}


void specialKeys(int key, int x, int y) {
	switch ((int)key) {

	case GLUT_KEY_DOWN: p1x -= 5; break;
	case GLUT_KEY_UP: p1x += 5; break;
	case GLUT_KEY_F1: flags[0] = !flags[0]; break;
	case GLUT_KEY_F2: flags[1] = !flags[1]; break;
	case GLUT_KEY_F3: flags[2] = !flags[2]; break;
	case GLUT_KEY_F4: flags[3] = !flags[3]; break;
	case GLUT_KEY_F5: flags[4] = !flags[4]; break;
	case GLUT_KEY_SHIFT_L: rotate_x -= 5; break;
	case GLUT_KEY_SHIFT_R: rotate_x += 5; break;
	case GLUT_KEY_CTRL_L: rotate_y -= 5; break;
	case GLUT_KEY_CTRL_R: rotate_y += 5; break;
	case GLUT_KEY_ALT_L: rotate_z -= 5; break;
	case GLUT_KEY_ALT_R: rotate_z += 5; break;
	}
	glutPostRedisplay();
}

int main(int argc, char ** argv) {
	glutInit(&argc, argv);

	glutInitWindowPosition(50, 50);
	glutInitWindowSize(800, 600);
	glutInitDisplayMode(GLUT_DEPTH | GLUT_DOUBLE | GLUT_RGBA);

	glutCreateWindow("Car");
	Init();
	glEnable(GL_COLOR_MATERIAL);

	glutReshapeFunc(Reshape);
	glutDisplayFunc(Update);
	glutSpecialFunc(specialKeys);

	glutMainLoop();
	return 0;
}