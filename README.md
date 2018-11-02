﻿# MMCS_332

## Module 1

### Task 1:
Преобразовать изображение из RGB в оттенки серого. Реализовать два варианта формулы: с равными  весами и с учетом разных вкладов R, G и B в интенсивность. Затем найти разность полученных полутоновых изображений. Построить гистограмму интенсивности.

### Task 2: 
Выделить из полноцветного изображения один из каналов R, G, B  и вывести результат. Построить гистограмму по цветам.

### Task 3: 
Преобразовать изображение из RGB в HSV. Добавить возможность изменять значения оттенка, насыщенности и яркости. Результат сохранять в файл, предварительно преобразовав обратно.

## Module 2: Рекурсивный алгоритм заливки на основе серий пикселов (линий)

Область рисуется мышкой. Область произвольной формы. Внутри могут быть отверстия. Точка, с которой начинается заливка, задается щелчком мыши.

### Task 1a: Заливка заданным цветом.

### Task 1b: Заливка рисунком из графического файла. 
Файл можно загрузить встроенными средствами и затем считывать точки изображения для использования в заливке.

### Task 2: Выделение границы связной области.
На вход подается изображение. Граница связной области задается одним цветом. Имея начальную точку границы организовать ее обход, занося точки в список в порядке обхода.
- Начальную точку границы можно получать любым способом.
- Для контроля полученную границу прорисовать поверх исходного изображения.

### Additional:
Реализация алгоритма заливки области, при помощи алгоритма выделения границы

## Module 3: Аффиные преобразования на плоскости

### Task:
- В программе должны присутствовать следующие примитивы: точка, ребро (отрезок), многоугольник.
- В программе предусмотреть рисование единственного примитива (ребра, многоугольника).

Программа должна содержать следующие возможности:
- Задать текущий примитив.
- Очистить сцену.
- Применение аффинных преобразований к примитиву: смещение, поворот, масштаб, с указанием параметров преобразования. Преобразования должны быть реализованы матрицами!
- Поворот ребра на 90 градусов вокруг своего центра.
- Поворот примитива на заданный угол вокруг другой точки (задание точки мышкой).
- Поиск точки пересечения двух ребер (добавление второго ребра мышкой, динамически).

Программа должна позволять выполнить следующие проверки:
- Принадлежит ли точка выпуклому многоугольнику (задание точки мышкой).
- Принадлежит ли точка невыпуклому многоугольнику (задание точки мышкой).
- Классифицировать положение точки относительно ребра (задание точки мышкой).

## Module 4

### Task 1: L-системы
Реализовать программу для построения фрактальных узоров посредством L-систем.
- Описание L-систем задается в текстовом файле.
- Реализовать возможность разветвления в системе (скобки).
- Предусмотреть масштабирование получаемого набора точек (должен помещаться в окне).

### Task 2: Diamond-square
Реализовать алгоритм midpoint displacement для визуализации горного массива.
Необходимо отображать результаты последовательных шагов алгоритма. Программа должна позволять изменять параметры построения ломаной.

### Task 3: Кубические сплайны Безье
Реализовать программу для визуализации составной кубической кривой Безье. 
Программа должна позволять добавлять, удалять  и перемещать опорные точки.

## Module 5: Аффинные преобразования в пространстве. Проецирование

- В программе должны присутствовать следующие классы: точка, прямая (ребро), многоугольник (грань), многогранник.

- Программа должна содержать следующие возможности:
- Отображение одного из правильных многогранников: тетраэдр, гексаэдр, октаэдр, икосаэдр*, додекаэдр*.
- Применение аффинных преобразований: смещение, поворот, масштаб, с указанием параметров преобразования. Преобразования должны быть реализованы матрицами!
- Отражение относительно выбранной координатной плоскости.
- Масштабирование многогранника относительно своего центра.
- Вращение многогранника вокруг прямой проходящей через центр многогранника, параллельно выбранной координатной оси.
- Поворот вокруг произвольной (заданной координатами двух точек) прямой на заданный угол.

Программа должна позволять отобразить сцену в одной из заданных проекций (преобразования должны быть реализованы матрицами):
- перспективной;
- изометрической;
- ортографической (на выбранную координатную плоскость).
	
## Module 6: Построение трёхмерных моделей

### Task 1: Загрузка и сохранение трёхмерной модели из файла
Формат модели должен содержать данные о гранях. Формат файла выбирается программистом.
Необходимо отобразить загруженную модель, позволить применять к ней аффинные преобразования. 

### Task 2: Построение фигуры вращения
Фигура вращения задаётся тремя параметрами: образующей (набор точек), осью вращения и количеством разбиений. Угол вращений можно вычислить, поделив 360° на количество разбиений.
Программа должна давать возможность задать образующую и построить фигуру вращения относительно выбранной координатной оси с заданным количеством разбиений. Формат модели должен содержать данные о гранях.
Необходимо отобразить полученную модель, позволить применять к ней аффинные преобразования.

### Task 3: Построение графика двух переменных
Сегмент поверхности задаётся функцией f(x, y) = z, диапазонами отсечения [x0, x1], [y0, y1] и количеством разбиений по осям (шагом).
Программа должна позволять строить сегмент поверхности, заданный выбранной функцией на заданном диапазоне с заданным количеством разбиений. Формат модели должен содержать данные о гранях. Диапазоны и разбиения можно задавать идентичными для X и Y.
Необходимо отобразить полученную модель, позволить применять к ней аффинные преобразования.

## Module 7: Отсечение нелицевых граней

### Task 1: Отсечение нелицевых граней
Изобразить вращающийся объект (созданный ранее) с отсечением нелицевых граней:
- Добавить к каждой грани вектор нормали, направленный вовне, относительно объекта. Вектор нормали вычисляется, как векторное произведение векторов, лежащих в плоскости грани.
- Задать вектор обзора. Добавить возможность его изменения.
- Реализовать отсечение нелицевых граней с учетом вектора нормали и направления обзора. 

### Task 2: Алгоритм Z-буфера
Реализовать алгоритм z-буфера для удаления невидимых граней. Направление обзора может быть фиксированным.
- Добавить возможность применять преобразования к отображаемому объекту.
- Если объект выпуклый, то необходимо добавить еще несколько объектов для демонстрации корректной работы алгоритма в случае перекрытия.

### Task 3: Камера
Добавить объект-камеру. 
- Камера задается своим положением, направлением обзора и матрицей проекционного преобразования. 
- Получить изображение с камеры, вращая её вокруг статического объекта. 

## UnionPolygons: Объединение выпуклых полигонов