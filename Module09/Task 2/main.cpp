#include <GL\glew.h>
#include <GL\freeglut.h>

static int w = 800, h = 800;

double rotate_x = 0;
double rotate_y = 0;
double rotate_z = 0;
int mode = 0;
int projection = 0;

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

//first
float x = 4.5, y = 1.8, z = 0;
//second
float x1 = 6.0, y1 = -1.0, z1 = 0;
//third
float x2 = 3.0, y2 = -1.0, z2 = 0;

//center
float o1 = 4.5, o2 = 0, o3 = 0;

void drawWireCube() {
	glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);
	if(!projection){
		glMatrixMode(GL_PROJECTION);
		glLoadIdentity();
		glOrtho(-10, 10, -10, 10, -10, 10);
		glMatrixMode(GL_MODELVIEW);
		glLoadIdentity();
	}
	else {
		glMatrixMode(GL_PROJECTION);
		glLoadIdentity();
		gluPerspective(65.0f, w / h, 0.1f, 1000.0f);
		glMatrixMode(GL_MODELVIEW);
		glLoadIdentity();
		gluLookAt(0.0f, 0.0f, 15.0f, 0.0, 0.0, 0.0, 0.0, 1.0, 0.0);
	}
	
	glPushMatrix();
	
	if(!mode)
		glTranslated(x, y, z);
	if (mode == 2)
		glTranslated(o1, o2, o3);

	glRotatef(rotate_x, 1.0, 0.0, 0.0);
	glRotatef(rotate_y, 0.0, 1.0, 0.0);
	glRotatef(rotate_z, 0.0, 0.0, 1.0);

	if(mode==1)
		glTranslated(x, y, z);

	if (mode == 2) {
		glTranslated(-o1, -o2, -o3);
		glTranslated(x, y, z);
	}

	glColor3f(238/255.0, 201/255.0, 0);
	glutSolidCube(2);

	glPopMatrix();
	glPushMatrix();
	if (!mode)
		glTranslated(x1, y1, z1);
	if (mode == 2)
		glTranslated(o1, o2, o3);

	glRotatef(rotate_x, 1.0, 0.0, 0.0);
	glRotatef(rotate_y, 0.0, 1.0, 0.0);
	glRotatef(rotate_z, 0.0, 0.0, 1.0);

	if (mode == 1)
		glTranslated(x1, y1, z1);
	if (mode == 2) {
		glTranslated(-o1, -o2, -o3);
		glTranslated(x1, y1, z1);
	}

	glColor3f(184 / 255.0, 134 / 255.0, 11 / 255.0);
	glutSolidCube(2);

	glPopMatrix();
	glPushMatrix();
	
	if (!mode) 
		glTranslated(x2, y2, z2);
	if (mode == 2)
		glTranslated(o1, o2, o3);

	glRotatef(rotate_x, 1.0, 0.0, 0.0);
	glRotatef(rotate_y, 0.0, 1.0, 0.0);
	glRotatef(rotate_z, 0.0, 0.0, 1.0);

	if (mode == 1) 
		glTranslated(x2, y2, z2);

	if (mode == 2) {
		glTranslated(-o1, -o2, -o3);
		glTranslated(x2, y2, z2);
	}
	glColor3f(0.75, 0.75, 0.75);
	glutSolidCube(2);
	
	glFlush();
	glutSwapBuffers();
}

void specialKeys(int key, int x, int y) {
	switch((int)key) {
		case GLUT_KEY_UP: rotate_x += 5; break;
		case GLUT_KEY_DOWN: rotate_x -= 5; break;
		case GLUT_KEY_RIGHT: rotate_y += 5; break;
		case GLUT_KEY_LEFT: rotate_y -= 5; break;
		case GLUT_KEY_PAGE_UP: rotate_z += 5; break;
		case GLUT_KEY_PAGE_DOWN: rotate_z -= 5; break;
		case GLUT_KEY_F1: rotate_x = rotate_y = rotate_z = mode = 0; break;
		case GLUT_KEY_F2: rotate_x = rotate_y = rotate_z = 0; mode = 1; break;
		case GLUT_KEY_F3: rotate_x = rotate_y = rotate_z = 0; mode = 2; break;
		case GLUT_KEY_SHIFT_L: rotate_x = rotate_y = rotate_z = 0; projection = 0; break;
		case GLUT_KEY_CTRL_L: rotate_x = rotate_y = rotate_z = 0; projection = 1; break;
	}
	glutPostRedisplay();
}

int main(int argc, char** argv) {
	glutInit(&argc, argv);
	glutInitWindowPosition(250, 0);
	glutInitWindowSize(w, h);
	glutInitDisplayMode(GLUT_RGBA | GLUT_DOUBLE | GLUT_DEPTH);
	glutCreateWindow("OpenGL");
	glutReshapeFunc(Reshape);
	glutDisplayFunc(drawWireCube);
	glutSpecialFunc(specialKeys);
	
	Init();

	glutMainLoop();
}