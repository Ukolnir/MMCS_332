

#include <iostream>
#include <Windows.h>
#include "D:\Документы\OneDrive\Документы\7 семестр\комп. графика\freeglut\include\GL\freeglut.h"
#include "D:\Документы\OneDrive\Документы\7 семестр\комп. графика\freeglut\include\GL\SOIL.h"

unsigned int texture;
unsigned int photo_tex;
int w, h;

void LoadImage() {
	texture = SOIL_load_OGL_texture("D:/Games/road.jpg", SOIL_LOAD_AUTO, SOIL_CREATE_NEW_ID, SOIL_FLAG_MIPMAPS | SOIL_FLAG_INVERT_Y | SOIL_FLAG_NTSC_SAFE_RGB | SOIL_FLAG_COMPRESS_TO_DXT);
	glGenTextures(1, &texture);
	glBindTexture(GL_TEXTURE_2D, texture);
}

/*#define checkImageWidth 64 
#define checkImageHeight 64 
GLubyte checkImage[checkImageHeight][checkImageWidth][4];
GLuint texName;
void makeCheckImage()
{
	int i, j, c;
	for (i = 0; i<checkImageHeight; i++)
	{
		for (j = 0; j<checkImageWidth; j++)
		{
			c = (((i & 0x8) == 0) ^ ((j & 0x8) == 0)) * 255;
			checkImage[i][j][0] = (GLubyte)c;
			checkImage[i][j][1] = (GLubyte)c;
			checkImage[i][j][2] = (GLubyte)c;
			checkImage[i][j][3] = (GLubyte)255;
		}
	}

}
*/
void Init(void) {
	glClearColor(0.0f, 0.0f, 0.0f, 1.0f);
	/*makeCheckImage();
	glPixelStorei(GL_UNPACK_ALIGNMENT, 1);
	glGenTextures(1, &texName);
	glBindTexture(GL_TEXTURE_2D, texName);
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_S, GL_REPEAT);
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_T, GL_REPEAT);
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_NEAREST);
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_NEAREST);
	glTexImage2D(GL_TEXTURE_2D, 0, GL_RGBA, checkImageWidth, checkImageHeight,
		0, GL_RGBA, GL_UNSIGNED_BYTE, checkImage);
		*/
}

void Update(void) {
	double angle = 1;
	angle += 0.5f;

	gluLookAt(100.0f, 100.0f, 100.0f, 60.0f, 50.0f, 40.0f, 0.0f, 1.0f, 0.0f);
	glRotatef(angle, 0.0f, 1.0f, 0.0f);

	LoadImage();
	glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);
	glBindTexture(GL_TEXTURE_2D, texture);
	glEnable(GL_TEXTURE_2D);

	glBegin(GL_POLYGON);
	glTexCoord2f(0, 0); glVertex3f(0, 0, 0);
	glTexCoord2f(0, 1); glVertex3f(0, 0, 320);
	glTexCoord2f(1, 1); glVertex3f(320, 0, 320);
	glTexCoord2f(1, 0); glVertex3f(320, 0, 0);
	glEnd();
	glDisable(GL_TEXTURE_2D);

	/*glColor3f(1, 0.2, 0.15);
	glutSolidCube(20);*/
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