#include <string>
#include <vector>
#include <fstream>
#include <iterator>
#include <sstream>

int main()
{

	std::vector<unsigned int> vertex_indices, uv_indices, normal_indices;

	std::ifstream infile("C:\\Users\\user\\Desktop\\elefante.obj");
	std::ofstream ofile("C:\\Users\\user\\Desktop\\elefante1.obj");
	std::string line;

	int obj_scale = 1;

	while (getline(infile, line)) {
		std::stringstream ss(line);
		std::string lineHeader;
		getline(ss, lineHeader, ' ');
		if (lineHeader == "f") {
			int vertex_index, uv_index, normal_index;
			char slash;
			int cnt = 0;
			ofile << "f ";
			while (ss >> vertex_index >> slash >> uv_index >> slash >> normal_index) 
			{
				ofile << vertex_index << slash << uv_index << slash << normal_index << " ";
				cnt++;
			}
			if (cnt == 3)
				ofile << vertex_index << slash << uv_index << slash << normal_index << std::endl;
			else
				ofile << std::endl;
		}
	}

}