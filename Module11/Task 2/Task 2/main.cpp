#include <GL\glew.h>
#include <GL\freeglut.h>
#include <GL\freeglut_ext.h>
#include <GL\freeglut_std.h>
#include <GL\GLU.h>
#include <GL\GL.h>
//#include <GL\wglew.h>
//#include <GL\glxew.h>
#include <string>
#include <vector>
#include <vec3.hpp>
#include <vec2.hpp>
#include <iostream>

#define _CRT_SECURE_NO_WARNINGS

using std::string;
using std::vector;
using glm::vec2;
using glm::vec3;
using std::cout;

static int w = 800, h = 800;

void loadOBJ(string path, vector <vec3> & out_vertices, vector <vec2> & out_uvs,
	vector <vec3> & out_normals) {

	vector< unsigned int > vertexIndices, uvIndices, normalIndices;
	vector< vec3 > temp_vertices;
	vector< vec2 > temp_uvs;
	vector< vec3 > temp_normals;

	FILE *file;
	fopen_s(&file, path.c_str(), "r");

	while (1) {
		char lineHeader[256];
		int res = fscanf(file, "%s", lineHeader);
		//if (res == EOF)
			//break;
		if (strcmp(lineHeader, "v") == 0) {
			vec3 vertex;
			fscanf(file, "%f %f %f\n", &vertex.x, &vertex.y, &vertex.z);
			temp_vertices.push_back(vertex);
		}
		else if (strcmp(lineHeader, "vt") == 0) {
			vec2 uv;
			fscanf(file, "%f %f\n", &uv.x, &uv.y);
			temp_uvs.push_back(uv);
		}
		else if (strcmp(lineHeader, "vn") == 0) {
			vec3 normal;
			fscanf(file, "%f %f %f\n", &normal.x, &normal.y, &normal.z);
			temp_normals.push_back(normal);
		}
		else if (strcmp(lineHeader, "f") == 0) {
			string vertex1, vertex2, vertex3;
			unsigned int vertexIndex[3], uvIndex[3], normalIndex[3];
			int matches = fscanf(file, "%d/%d/%d %d/%d/%d %d/%d/%d\n", &vertexIndex[0], &uvIndex[0], &normalIndex[0], &vertexIndex[1], &uvIndex[1], &normalIndex[1], &vertexIndex[2], &uvIndex[2], &normalIndex[2]);

			vertexIndices.push_back(vertexIndex[0]);
			vertexIndices.push_back(vertexIndex[1]);
			vertexIndices.push_back(vertexIndex[2]);
			uvIndices.push_back(uvIndex[0]);
			uvIndices.push_back(uvIndex[1]);
			uvIndices.push_back(uvIndex[2]);
			normalIndices.push_back(normalIndex[0]);
			normalIndices.push_back(normalIndex[1]);
			normalIndices.push_back(normalIndex[2]);

			for (unsigned int i = 0; i < vertexIndices.size(); i++) {
				unsigned int vertexIndex = vertexIndices[i];
				vec3 vertex = temp_vertices[vertexIndex - 1];
				out_vertices.push_back(vertex);
			}

			for (unsigned int i = 0; i < uvIndices.size(); i++) {
				unsigned int uvIndex = uvIndices[i];
				vec2 uvs = temp_uvs[uvIndex - 1];
				out_uvs.push_back(uvs);
			}

			for (unsigned int i = 0; i < normalIndices.size(); i++) {
				unsigned int normalIndex = normalIndices[i];
				vec3 normal = temp_normals[normalIndex - 1];
				out_normals.push_back(normal);
			}
		}
	}
}

void Update(void) {
	glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);
	glutSwapBuffers();
}

void Reshape(int width, int height) {
	width = w; height = h;
}

void drawVBO() {
	GLuint vId;
	vector<vec3> VertexBuffer;
	int Count = 100;
}

struct TVertex {
	float X, Y, Z;
};

struct Texture {
	unsigned int id;
	string type;
};

struct TColor {
	int R, G, B;
	TColor() {
		R = 0; G = 0; B = 0;
	}
	TColor(int r, int g, int b) {
		R = r; G = g; B = b;
	}
};

vector<TVertex> VertexBuffer;
vector<TVertex> NormalsBuff;
vector<TColor> ColorBuffer;
vector<int> Indices;

GLuint vId, cId, nId, tId, iId;
GLuint vao;

void VBOinit() {
	int count = VertexBuffer.size() + 1;
	glGenBuffers(1, &vId);
	/*glBindBuffer(GL_ARRAY_BUFFER, vId);
	glBufferData(GL_ARRAY_BUFFER, sizeof(float) * 3 * count, &VertexBuffer[0], GL_STATIC_DRAW);

	glGenBuffers(1, &cId);
	glBindBuffer(GL_ARRAY_BUFFER, cId);
	glBufferData(GL_ARRAY_BUFFER, sizeof(int) * 3 * count, &ColorBuffer[0], GL_STATIC_DRAW);

	glGenBuffers(1, &nId);
	glBindBuffer(GL_ARRAY_BUFFER, nId);
	glBufferData(GL_ARRAY_BUFFER, sizeof(float) * 3 * count, &NormalsBuff[0], GL_STATIC_DRAW);

	glGenBuffers(1, &iId);
	glBindBuffer(GL_ELEMENT_ARRAY_BUFFER, iId);
	count = Indices.size() + 1;
	glBufferData(GL_ELEMENT_ARRAY_BUFFER, sizeof(int) * count, &Indices[0], GL_STATIC_DRAW);

	glBindBuffer(GL_ARRAY_BUFFER, 0);
	glBindBuffer(GL_ELEMENT_ARRAY_BUFFER, 0);

	/////VAO
	
	glGenVertexArrays(1, &vao);
	glBindVertexArray(vao);
	glEnableClientState(GL_VERTEX_ARRAY);
	glBindBuffer(GL_ARRAY_BUFFER, vId);
	glVertexPointer(3, GL_FLOAT, 0, 0);
	glEnableClientState(GL_COLOR_ARRAY);
	glBindBuffer(GL_ARRAY_BUFFER, cId);
	glColorPointer(3, GL_INT, 0, 0);
	glEnableClientState(GL_NORMAL_ARRAY);
	glBindBuffer(GL_ARRAY_BUFFER, nId);
	glNormalPointer(GL_FLOAT, 0, 0);
	glBindBuffer(GL_ELEMENT_ARRAY_BUFFER, iId);
	glBindVertexArray(0);
	glBindBuffer(GL_ARRAY_BUFFER, 0);
	glBindBuffer(GL_ELEMENT_ARRAY_BUFFER, 0);


	*/

	/*
	glEnableClientState(GL_TEXTURE_COORD_ARRAY);
	glBindBuffer(GL_ARRAY_BUFFER, tId);
	glTexCoordPointer(2, GL_FLOAT, 0, nil);
	end;*/

}

/*Procedure VBOInit;
Var Count:integer;
begin
  Count:=high(VertexBuffer)+1;
  //Создаем буферы VBO

  glGenBuffers( 1, @tId );
  glBindBuffer(GL_ARRAY_BUFFER, tId );
  glBufferData(GL_ARRAY_BUFFER, sizeof(GLFLoat)*2*Count, @TexCoordsBuff[0], GL_STATIC_DRAW);
end;*/
void initCreate() {
	VertexBuffer = vector<TVertex>(6);
	NormalsBuff = vector<TVertex>(6);
	ColorBuffer = vector<TColor>(6);
	Indices = vector<int>(6);

	VertexBuffer[0].X = -1; VertexBuffer[0].Y = 1; VertexBuffer[0].Z = 0;
	VertexBuffer[1].X = -1; VertexBuffer[1].Y = -1; VertexBuffer[1].Z = 0;
	VertexBuffer[2].X = 1; VertexBuffer[2].Y = 1; VertexBuffer[2].Z = 0;
	VertexBuffer[3].X = 1; VertexBuffer[3].Y = 1; VertexBuffer[3].Z = 0;
	VertexBuffer[4].X = -1; VertexBuffer[4].Y = -1; VertexBuffer[4].Z = 0;
	VertexBuffer[5].X = 1; VertexBuffer[5].Y = -1; VertexBuffer[5].Z = 0;

	ColorBuffer[0] = TColor(255, 0, 0);
	ColorBuffer[1] = TColor(0, 255, 0);
	ColorBuffer[2] = TColor(0, 0, 255);
	ColorBuffer[3] = TColor(0, 0, 255);
	ColorBuffer[4] = TColor(0, 255, 0);
	ColorBuffer[5] = TColor(255, 255, 0);

	NormalsBuff[0].X = 0; NormalsBuff[0].Y = 0; NormalsBuff[0].Z = 1;
	NormalsBuff[1].X = 0; NormalsBuff[1].Y = 0; NormalsBuff[1].Z = 1;
	NormalsBuff[2].X = 0; NormalsBuff[2].Y = 0; NormalsBuff[2].Z = 1;
	NormalsBuff[3].X = 0; NormalsBuff[3].Y = 0; NormalsBuff[3].Z = 1;
	NormalsBuff[4].X = 0; NormalsBuff[4].Y = 0; NormalsBuff[4].Z = 1;
	NormalsBuff[5].X = 0; NormalsBuff[5].Y = 0; NormalsBuff[5].Z = 1;

	Indices[0] = 0; Indices[1] = 1; Indices[2] = 2;
	Indices[3] = 3; Indices[4] = 4; Indices[5] = 5;
}

void Init(void) {
	glClearColor(0.0f, 0.0f, 0.0f, 1.0f);

	glEnable(GL_DEPTH_TEST);
	glEnable(GL_LIGHTING);
	glEnable(GL_COLOR_MATERIAL);
	glEnable(GL_LIGHT0);
	glLightModelf(GL_LIGHT_MODEL_TWO_SIDE, GL_TRUE);
	glEnable(GL_NORMALIZE);
	initCreate();
}

/*
type
    procedure FormClose(Sender: TObject; var Action: TCloseAction);
    procedure GLDirectOpenGL1Render(Sender: TObject;
      var rci: TRenderContextInfo);
    procedure GLCadencer1Progress(Sender: TObject; const deltaTime,
      newTime: Double);
  private
    { Private declarations }
  public
    { Public declarations }
  end;
Type

  TTexCoord2 = record
     s,t: single;
  end;
var
  Form1: TForm1;
 
  TexCoordsBuff: Array of TTexCoord2;

procedure TForm1.FormCreate(Sender: TObject);
begin
   //Создаем буферы под 4 вершины четырехугольник
   Setlength(TexCoordsBuff,6);

   //Задаем 4 вершины и атрибуты для каждой из них:
   TexCoordsBuff[0].s:=0; TexCoordsBuff[0].t:=1;
   TexCoordsBuff[1].s:=0; TexCoordsBuff[1].t:=0;
   TexCoordsBuff[2].s:=1; TexCoordsBuff[2].t:=1;
   TexCoordsBuff[3].s:=1; TexCoordsBuff[3].t:=1;
   TexCoordsBuff[4].s:=0; TexCoordsBuff[4].t:=0;
   TexCoordsBuff[5].s:=1; TexCoordsBuff[5].t:=0;
   //Инициализируем наш буфер
   GLSceneViewer1.Buffer.RenderingContext.Activate;
   VBOInit;
   Ready:=true;
end;

Procedure VBODraw;
Var Count:integer;
Begin
  Count:=high(Indices)+1;
  glBindVertexArray(vao);
  glDrawElements(GL_TRIANGLES, Count, GL_UNSIGNED_BYTE, nil);
  glBindVertexArray(0);
End;

Procedure VBOFree;
Begin
  glBindVertexArray(0);
  glDeleteVertexArrays(1,@vao);

  glDeleteBuffers(1,@vId);
  glDeleteBuffers(1,@cId);
  glDeleteBuffers(1,@nId);
  glDeleteBuffers(1,@tId);
End;

Function SetRGB(r,g,b:byte):TRGBTriple;
begin
  Result.rgbtBlue:=b;
  Result.rgbtGreen:=g;
  Result.rgbtRed:=r;
end;



procedure TForm1.FormClose(Sender: TObject; var Action: TCloseAction);
begin
  Ready:=false;
  GLSceneViewer1.Buffer.RenderingContext.Activate;
  VBOFree;
end;

procedure TForm1.GLDirectOpenGL1Render(Sender: TObject;
  var rci: TRenderContextInfo);
begin
  if not Ready then exit;
//  glEnable(GL_COLOR_MATERIAL);
  glMaterialLibrary1.Materials[0].Apply(rci);
  VBODraw;
//  glDisable(GL_COLOR_MATERIAL);
  glMaterialLibrary1.Materials[0].unApply(rci);
end;

procedure TForm1.GLCadencer1Progress(Sender: TObject; const deltaTime,
  newTime: Double);
begin
   GLSceneViewer1.Invalidate;
end;

end.
*/




int main(int argc, char** argv) {
	glutInit(&argc, argv);
	glutInitWindowPosition(250, 0);
	glutInitWindowSize(w, h);
	glutInitDisplayMode(GLUT_RGBA | GLUT_DOUBLE | GLUT_DEPTH);
	glutCreateWindow("OpenGL");
	glutReshapeFunc(Reshape);
	glutDisplayFunc(drawVBO);

	Init();
	
	glutMainLoop();
}