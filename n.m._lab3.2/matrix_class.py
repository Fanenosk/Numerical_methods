import matrix_transformations as mt


##########
# классы
class Matrix(object):
    """docstring for Matrix"""
    matrix = [[0]]

    def __init__(self, matrix: list = None, rows: int = 0, cols: int = 0):
        # создать матрицу из 2д списка
        # создать нулевую матрицу размера n*m

        if matrix is None:
            assert isinstance(rows, int)
            assert isinstance(cols, int)
            self.matrix = mt.zeroes(rows, cols)
        else:
            assert isinstance(matrix, list)
            self.matrix = matrix
        self.cols = len(self.matrix[0])
        self.rows = len(self.matrix)

    def __add__(self, other):
        assert isinstance(other, Matrix)
        return Matrix(mt.add(self.matrix, other.matrix))

    def __sub__(self, other):
        assert isinstance(other, Matrix)
        return Matrix(mt.subtract(self.matrix, other.matrix))

    def __mul__(self, other):
        if isinstance(other, Matrix):
            # умножение матриц
            return Matrix(mt.mul(self.matrix, other.matrix))
        else:
            # умножение матрицы на число
            assert isinstance(other, (float, int))
            n = self.rows
            m = self.cols
            A = zeroes(n, m)
            for i in range(n):
                for j in range(m):
                    A[i][j] = self.matrix[i][j] * other
            return A

    def __str__(self):
        return str(mt.format(self.matrix))

    def __getitem__(self, key):
        # Доступ к элементу
        return self.matrix[key]

    def __eq__(self, other):
        # знак равенства
        if not isinstance(other, Matrix):
            return False
        return self.matrix == other.matrix

    def copy(self):
        # Создание копии объекта
        return Matrix(mt.copy(self.matrix))

    def transpose(self):
        # Транспонирование матрицы
        return Matrix(mt.transpose(self.matrix))

    def __call__(self, *args, **kwargs):
        # вызывает элементы матрицы как функции
        B = Matrix(mt.zeroes(self.rows, self.cols))
        for i in range(self.rows):
            for j in range(self.cols):
                B[i][j] = self.matrix[i][j](*args, **kwargs)
        return B


###########
# методы

def ident_matrix(n: int) -> Matrix:
    # возвращает квадратную единичную матрицу
    A = zeroes(n, n)
    for i in range(n):
        A[i][i] = 1
    return A


def zeroes(n: int, m: int) -> Matrix:
    # нулевая матрица n*m
    return Matrix([([0.] * m).copy() for i in range(n)])


def norm2(A: Matrix) -> float:
    # евклидова норма матрицы
    # (2 - норма)
    n = A.rows
    m = A.cols
    summ = 0
    for i in range(n):
        for j in range(m):
            summ += A[i][j] ** 2
    return summ ** 0.5
