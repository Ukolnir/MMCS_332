
#include <GL\glew.h>
#include <GL\freeglut.h>

#include<functional>
#include<vector>
#include<iostream>
#include<random>

using namespace std;

/**
* glVertex2f(float x, float y).
* The point (0.0, 0.0) represents the middle of the window (not the top left corner).
* The "2f" suffix means 2 values of float type (x and y).
*/

double rotate_x = 0;
double rotate_y = 0;
double rotate_z = 0;

static int w = 0, h = 0;

vector<function<void(void)>> funs;
int numfun = 0;

vector<double> curr_color;

vector<double> rand_color()
{
    vector<double> v;
    for (int i = 0; i < 12; ++i)
    {
        int r = rand() % 1000;
        v.push_back((double)r / 1000);
    }
    return v;
}


void renderWireCube() {
    glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);
    glLoadIdentity();
    glTranslatef(0.2, 0.0, 0.0);
    glRotatef(rotate_x, 1.0, 0.0, 0.0);
    glRotatef(rotate_y, 0.0, 1.0, 0.0);
    glRotatef(rotate_z, 0.0, 0.0, 1.0);

    glTranslatef(0.3, 0.0, 0.0);
    glColor3f(0.0f, 1.0f, 0.0f);      glutSolidCube(0.3);
    glTranslatef(0.3, 0.0, 0.0);
    glColor3f(0.75f, 0.0f, 0.0f);     glutSolidCube(0.15);
    glTranslatef(0.14, 0.0, 0.0);
    glColor3f(0.0f, 0.0f, 0.55f);     glutSolidCube(0.07);
    glFlush();
    glutSwapBuffers();
}

void callFun()
{
    funs[numfun % funs.size()]();
}

void specialKeys(int key, int x, int y) 
{
    switch(key) 
    {
case GLUT_KEY_UP: rotate_x += 5; break;
case GLUT_KEY_DOWN: rotate_x -= 5; break;
case GLUT_KEY_RIGHT: rotate_y += 5; break;
case GLUT_KEY_LEFT: rotate_y -= 5; break;
case GLUT_KEY_HOME: rotate_z += 5; break;
case GLUT_KEY_END: rotate_z -= 5; break;
    }
    glutPostRedisplay();
}

void specialMouse(int button, int state, int x, int y)
{
    if (state == GLUT_DOWN)
    {
        ++numfun;
        curr_color = rand_color();
    }
}

void Init()
{
    glClearColor(0.0f, 0.0f, 1.0f, 1.0f);
}

void Update()
{
    glClear(GL_COLOR_BUFFER_BIT);
    glutSwapBuffers();
}

void Reshape(int width, int height)
{
    w = width; h = height;
}


int main(int argc, char** argv) 
{
    curr_color = rand_color();
    funs.push_back(renderWireCube);

    glutInit(&argc, argv);
    glutInitDisplayMode(GLUT_SINGLE);
    glutInitWindowSize(600, 600);                    // window size
    glutInitWindowPosition(50, 50);                // distance from the top-left screen
    glutCreateWindow("LoooooooooooooL :D");    // message displayed on top bar window
    glutDisplayFunc(callFun);
    glutSpecialFunc(specialKeys);
    glutMouseFunc(specialMouse);
    glutMainLoop();
    return 0;
    
}