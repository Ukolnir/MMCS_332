// BadproG.com
#include <GL\glew.h>
#include <GL\freeglut.h>

/**
* glVertex2f(float x, float y).
* The point (0.0, 0.0) represents the middle of the window (not the top left corner).
* The "2f" suffix means 2 values of float type (x and y).
*/

double rotate_x = 0;
double rotate_y = 0;
double rotate_z = 0;

void displayMe(void) 
{
    glClear(GL_COLOR_BUFFER_BIT);
    glBegin(GL_POLYGON);
    glVertex2f(0.0, 0.0);                    // bottom left
    glVertex2f(0.5, 0.0);                    // bottom right
    glVertex2f(0.5, 0.5);                    // top right
    glVertex2f(0.0, 0.5);                    // top left
    glEnd();
    glFlush();
}

void renderRectangle()
{
    glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);
    glLoadIdentity();
    glRotatef(rotate_z, 0.0, 0.0, 1.0);
    glBegin(GL_QUADS);
    glColor3f(1.0, 0.0, 0.0); glVertex2f(-0.5f, -0.5f);
    glColor3f(0.0, 1.0, 0.0); glVertex2f(-0.5f, 0.5f);
    glColor3f(0.0, 0.0, 1.0); glVertex2f(0.5f, 0.5f);
    glColor3f(1.0, 1.0, 1.0); glVertex2f(0.5f, -0.5f);
    glEnd();
    glFlush(); glutSwapBuffers();
}

void specialKeys(int key, int x, int y) 
{
    switch(key) 
    {
case GLUT_KEY_UP: rotate_x += 5; break;
case GLUT_KEY_DOWN: rotate_x -= 5; break;
case GLUT_KEY_RIGHT: rotate_y += 5; break;
case GLUT_KEY_LEFT: rotate_y -= 5; break;
case GLUT_KEY_PAGE_UP: rotate_z += 5; break;
case GLUT_KEY_PAGE_DOWN: rotate_z -= 5; break;
    }
    glutPostRedisplay();
}

int main(int argc, char** argv) 
{
    glutInit(&argc, argv);
    glutInitDisplayMode(GLUT_SINGLE);
    glutInitWindowSize(300, 300);                    // window size
    glutInitWindowPosition(500, 500);                // distance from the top-left screen
    glutCreateWindow("LoooooooooooooL :D");    // message displayed on top bar window
    glutDisplayFunc(renderRectangle);
    glutSpecialFunc(specialKeys);
    glutMainLoop();
    return 0;
}