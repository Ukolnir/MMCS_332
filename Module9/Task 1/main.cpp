
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

void renderRectangle()
{
    glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);
    glLoadIdentity();
    glRotatef(rotate_x, 1.0, 0.0, 0.0);
    glRotatef(rotate_y, 0.0, 1.0, 0.0);
    glRotatef(rotate_z, 0.0, 0.0, 1.0);
    glBegin(GL_QUADS);
    glColor3f(curr_color[0], curr_color[1], curr_color[2]); glVertex2f(-0.5f, -0.5f);
    glColor3f(curr_color[3], curr_color[4], curr_color[5]); glVertex2f(-0.5f, 0.5f);
    glColor3f(curr_color[6], curr_color[7], curr_color[8]); glVertex2f(0.5f, 0.5f);
    glColor3f(curr_color[9], curr_color[10], curr_color[11]); glVertex2f(0.5f, -0.5f);
    glEnd();
    glFlush(); glutSwapBuffers();
}

void renderTriangle()
{
    glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);
    glLoadIdentity();
    glRotatef(rotate_x, 1.0, 0.0, 0.0);
    glRotatef(rotate_y, 0.0, 1.0, 0.0);
    glRotatef(rotate_z, 0.0, 0.0, 1.0);
    glBegin(GL_TRIANGLES);
    glColor3f(curr_color[0], curr_color[1], curr_color[2]); glVertex2f(-0.5f, -0.5f);
    glColor3f(curr_color[3], curr_color[4], curr_color[5]); glVertex2f(-0.5f, 0.5f);
    glColor3f(curr_color[6], curr_color[7], curr_color[8]); glVertex2f(0.5f, 0.5f);

    glEnd();
    glFlush(); glutSwapBuffers();
}

void renderWireSphere() {
    glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);
    glLoadIdentity();
    glRotatef(rotate_x, 1.0, 0.0, 0.0);
    glRotatef(rotate_y, 0.0, 1.0, 0.0);
    glRotatef(rotate_z, 0.0, 0.0, 1.0);
    glutWireSphere(0.5, 10, 10);
    glFlush();
    glutSwapBuffers();
}

void renderWireCube() {
    glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);
    glLoadIdentity();
    glRotatef(rotate_x, 1.0, 0.0, 0.0);
    glRotatef(rotate_y, 0.0, 1.0, 0.0);
    glRotatef(rotate_z, 0.0, 0.0, 1.0);
    glutWireCube(1);
    glFlush();
    glutSwapBuffers();
}

void renderWireCone() {
    glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);
    glLoadIdentity();
    glRotatef(rotate_x, 1.0, 0.0, 0.0);
    glRotatef(rotate_y, 0.0, 1.0, 0.0);
    glRotatef(rotate_z, 0.0, 0.0, 1.0);
    glutWireCone(0.5, 0.5, 10, 10);
    glFlush();
    glutSwapBuffers();
}

void renderWireTorus() {
    glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);
    glLoadIdentity();
    glRotatef(rotate_x, 1.0, 0.0, 0.0);
    glRotatef(rotate_y, 0.0, 1.0, 0.0);
    glRotatef(rotate_z, 0.0, 0.0, 1.0);
    glutWireTorus(0.5, 0.5, 10, 10);
    glFlush();
    glutSwapBuffers();
}

void renderWireOctahedron() {
    glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);
    glLoadIdentity();
    glRotatef(rotate_x, 1.0, 0.0, 0.0);
    glRotatef(rotate_y, 0.0, 1.0, 0.0);
    glRotatef(rotate_z, 0.0, 0.0, 1.0);
    glutWireOctahedron();
    glFlush();
    glutSwapBuffers();
}

void renderWireTetrahedron() {
    glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);
    glLoadIdentity();
    glRotatef(rotate_x, 1.0, 0.0, 0.0);
    glRotatef(rotate_y, 0.0, 1.0, 0.0);
    glRotatef(rotate_z, 0.0, 0.0, 1.0);
    glutWireTetrahedron();
    glFlush();
    glutSwapBuffers();
}

void renderWireIcosahedron() {
    glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);
    glLoadIdentity();
    glRotatef(rotate_x, 1.0, 0.0, 0.0);
    glRotatef(rotate_y, 0.0, 1.0, 0.0);
    glRotatef(rotate_z, 0.0, 0.0, 1.0);
    glutWireIcosahedron();
    glFlush();
    glutSwapBuffers();
}

void renderWireTeapot() {
    glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);
    glLoadIdentity();
    glRotatef(rotate_x, 1.0, 0.0, 0.0);
    glRotatef(rotate_y, 0.0, 1.0, 0.0);
    glRotatef(rotate_z, 0.0, 0.0, 1.0);
    glutWireTeapot(0.5);
    glFlush();
    glutSwapBuffers();
}

void renderWireTeacup() {
    glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);
    glLoadIdentity();
    glRotatef(rotate_x, 1.0, 0.0, 0.0);
    glRotatef(rotate_y, 0.0, 1.0, 0.0);
    glRotatef(rotate_z, 0.0, 0.0, 1.0);
    glutWireTeacup(0.5);
    glFlush();
    glutSwapBuffers();
}

void callFun()
{
    if (funs.size() > 0)
    {
        funs[numfun % funs.size()]();
    }
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
    funs.push_back(renderRectangle);
    funs.push_back(renderTriangle);
    funs.push_back(renderWireSphere);
    funs.push_back(renderWireCube);
    funs.push_back(renderWireCone);
    funs.push_back(renderWireTorus);
    funs.push_back(renderWireOctahedron);
    funs.push_back(renderWireTetrahedron);
    funs.push_back(renderWireIcosahedron);
    funs.push_back(renderWireTeapot);
    funs.push_back(renderWireTeacup);
    curr_color = rand_color();

    glutInit(&argc, argv);
    glutInitDisplayMode(GLUT_SINGLE);
    glutInitWindowSize(500, 500);                    // window size
    glutInitWindowPosition(100, 100);                // distance from the top-left screen
    glutCreateWindow("LoooooooooooooL :D");    // message displayed on top bar window
    glutDisplayFunc(callFun);
    glutSpecialFunc(specialKeys);
    glutMouseFunc(specialMouse);
    glutMainLoop();
    return 0;
    
}