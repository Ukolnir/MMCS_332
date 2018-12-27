#define _CRT_SECURE_NO_WARNINGS
#include <iostream>
#include <Windows.h>
#include "glew.h"
#include "wglew.h"
#include "freeglut.h"
#include "SOIL.h"
#include <string>
#include <vector>
#include <fstream>
#define GLM_ENABLE_EXPERIMENTAL
#include "glm\glm.hpp"
#include "glm\gtc\matrix_transform.hpp"
#include "glm\gtx\transform.hpp"
#include "glm\gtc\matrix_inverse.hpp"
#include "glm\gtc\type_ptr.hpp"
#include <sstream>
#include <map>

using namespace std;

struct PackedVertex{
public:
	glm::vec3 position;
	glm::vec2 uv;
	glm::vec3 normal;
	bool operator<(const PackedVertex that) const
	{
		return memcmp((void*)this, (void*)&that, sizeof(PackedVertex)) > 0;
	};
};

class Object {
public:
	float X, Y, Z, angle_X, angle_Y, angle_Z;
	string obj_path, tex_path;
	float obj_scale;
	glm::vec4 material_ambient, material_diffuse, material_specular, material_emission;
	
	GLuint textureID;

	bool without_text;

	glm::mat4 getModel() {
		glm::mat4 Model;
		Model = glm::translate(glm::mat4(1.0f), glm::vec3(X, Y, Z));
		Model = glm::rotate(Model, angle_X, glm::vec3(1.0f, 0.0f, 0.0f));
		Model = glm::rotate(Model, angle_Y, glm::vec3(0.0f, 1.0f, 0.0f));
		Model = glm::rotate(Model, angle_Z, glm::vec3(0.0f, 0.0f, 1.0f));
	
		return Model;
	}

	void _LoadImage() {
		textureID = SOIL_load_OGL_texture(tex_path.c_str(), SOIL_LOAD_AUTO, SOIL_CREATE_NEW_ID, SOIL_FLAG_MIPMAPS | SOIL_FLAG_INVERT_Y | SOIL_FLAG_NTSC_SAFE_RGB | SOIL_FLAG_COMPRESS_TO_DXT);
	}
private:
	void loadOBJ(std::vector<glm::vec3> & out_vertices, std::vector<glm::vec2> & out_uvs,
		std::vector<glm::vec3> & out_normals)
	{
		std::vector<unsigned int> vertex_indices, uv_indices, normal_indices;
		std::vector<glm::vec3> temp_vertices;
		std::vector<glm::vec2> temp_uvs;
		std::vector<glm::vec3> temp_normals;

		std::ifstream infile(obj_path);
		std::string line;

		while (getline(infile, line)) {
			std::stringstream ss(line);
			std::string lineHeader;
			getline(ss, lineHeader, ' ');
			if (lineHeader == "v") {
				glm::vec3 vertex;
				ss >> vertex.x >> vertex.y >> vertex.z;

				vertex.x *= obj_scale;
				vertex.y *= obj_scale;
				vertex.z *= obj_scale;
				temp_vertices.push_back(vertex);
			}
			else if (lineHeader == "vt") {
				glm::vec2 uv;
				ss >> uv.x >> uv.y;
				temp_uvs.push_back(uv);
			}
			else if (lineHeader == "vn") {
				glm::vec3 normal;
				ss >> normal.x >> normal.y >> normal.z;
				temp_normals.push_back(normal);
			}
			else if (lineHeader == "f") {
				int vertex_index, uv_index, normal_index = INT_MAX;
				char slash;
				int cnt = 0;
				while (ss >> vertex_index >> slash >> uv_index >> slash >> normal_index)
				{
					vertex_indices.push_back(vertex_index);
					
					uv_indices.push_back(uv_index);
					if (normal_index != INT_MAX)
						normal_indices.push_back(normal_index);
					else
						normal_indices.push_back(1);
					cnt++;
				}

				if (cnt == 3)
				{
					vertex_indices.push_back(vertex_index);
					uv_indices.push_back(uv_index);
					if (normal_index != INT_MAX)
						normal_indices.push_back(normal_index);
					else
						normal_indices.push_back(1);
					normal_indices.push_back(normal_index); cnt++;
				}
			}
		}

		// For each vertex of each triangle
		for (unsigned int i = 0; i < vertex_indices.size(); i++) {
			unsigned int vertexIndex = vertex_indices[i];
			glm::vec3 vertex = temp_vertices[vertexIndex - 1];
			out_vertices.push_back(vertex);

			unsigned int uvIndex = uv_indices[i];
			glm::vec2 uv = temp_uvs[uvIndex - 1];
			out_uvs.push_back(uv);

			unsigned int normalIndex = normal_indices[i];
			glm::vec3 normal = temp_normals[normalIndex - 1];
			out_normals.push_back(normal);
		}
	}

	bool getSimilarVertexIndex_fast(
		PackedVertex & packed,
		std::map<PackedVertex, unsigned short> & VertexToOutIndex,
		unsigned short & result
	) {
		std::map<PackedVertex, unsigned short>::iterator it = VertexToOutIndex.find(packed);
		if (it == VertexToOutIndex.end()) return false;
		else {
			result = it->second;
			return true;
		}
	}

	void indexVBO(
		std::vector<glm::vec3> & in_vertices,
		std::vector<glm::vec2> & in_uvs,
		std::vector<glm::vec3> & in_normals,

		std::vector<unsigned short> & out_indices,
		std::vector<glm::vec3> & out_vertices,
		std::vector<glm::vec2> & out_uvs,
		std::vector<glm::vec3> & out_normals
	) {
		std::map<PackedVertex, unsigned short> VertexToOutIndex;

		// For each input vertex
		for (unsigned int i = 0; i < in_vertices.size(); i++)
		{
			PackedVertex packed = { in_vertices[i], in_uvs[i], in_normals[i] };

			// Try to find a similar vertex in out_XXXX
			unsigned short index;
			bool found = getSimilarVertexIndex_fast(packed, VertexToOutIndex, index);

			if (found)
			{ // A similar vertex is already in the VBO, use it instead!
				out_indices.push_back(index);
			}
			else
			{ // If not, it needs to be added in the output data.
				out_vertices.push_back(in_vertices[i]);
				out_uvs.push_back(in_uvs[i]);
				out_normals.push_back(in_normals[i]);
				unsigned short newindex = (unsigned short)out_vertices.size() - 1;
				out_indices.push_back(newindex);
				VertexToOutIndex[packed] = newindex;
			}
		}
	}



public:	
	std::vector<unsigned short> indices;

	std::vector<glm::vec3> indexed_vertices;
	std::vector<glm::vec2> indexed_uvs;
	std::vector<glm::vec3> indexed_normals;

	void initDataBuffers() {
		std::vector<glm::vec3> vertices;
		std::vector<glm::vec2> uvs;
		std::vector<glm::vec3> normals;
		loadOBJ(vertices, uvs, normals);
		indexVBO(vertices, uvs, normals, indices, indexed_vertices, indexed_uvs, indexed_normals);
	}

	Object(string obj, string tex, float scale,
		float x, float y, float z, float a_x, float a_y, float a_z,
		glm::vec4 ambient, glm::vec4 diffuse, glm::vec4 specular, glm::vec4 emission
	)
		: obj_scale(scale), X(x), Y(y), Z(z), angle_X(a_x), angle_Y(a_y), angle_Z(a_z) {

		obj_path = obj;
		tex_path = tex;
		without_text = false;
		material_ambient = glm::vec4(ambient);
		material_diffuse = glm::vec4(diffuse);
		material_specular = glm::vec4(specular);
		material_emission = glm::vec4(emission);
	}

	Object(string obj, float scale,
		float x, float y, float z, float a_x, float a_y, float a_z,
		glm::vec4 ambient, glm::vec4 diffuse, glm::vec4 specular, glm::vec4 emission
	)
		: obj_scale(scale), X(x), Y(y), Z(z), angle_X(a_x), angle_Y(a_y), angle_Z(a_z) {

		obj_path = obj;

		without_text = true;

		material_ambient = glm::vec4(ambient);
		material_diffuse = glm::vec4(diffuse);
		material_specular = glm::vec4(specular);
		material_emission = glm::vec4(emission);
	}
};