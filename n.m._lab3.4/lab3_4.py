import numpy as np
import matrix_transformations as mt

def newtons_polynom(x, y, n):
    # пришлось переписывать полином, чтобы он хранил коэффициенты
    diff = mt.zeroes(n, n)
    for i in range(0, n):
        diff[i][i] = y[i]
        for j in range(i - 1, -1, -1):
            diff[j][i] = (diff[j + 1][i] - diff[j][i - 1])
            diff[j][i] /= (x[i] - x[j])
    coeff = [diff[0][i] for i in range(n)]
    return x, coeff, n


def newtons_derivative(polynom, point, order):
    # производная от заданного полинома заданной степени
    x, coeff, n = polynom

    def mul_deriv(operands, cur_order=order):
        res = 0.0
        if cur_order != 0:
            for i in range(n):
                if operands[i] != 0:
                    operands[i] = 0
                    res += mul_deriv(operands, cur_order - 1)
                    operands[i] = 1
        else:
            res = 1.0
            for i in range(n):
                if operands[i] != 0:
                    res *= (point - x[i])
        return res

    operands = [0.] * n
    for i in range(order):
        operands[i] = 1
    result = 0.0
    for i in range(order, n):
        result += coeff[i] * mul_deriv(operands)
        operands[i] = 1

    return result

def simple_derivative(x, y, val, order):
    n = len(x)
    k = 0
    try:
        while x[k + 1] < val:
            k += 1
    except IndexError:
        print("Значение не входит в заданный интервал")
        exit(1)

    if k + 1 > n + 1:
        k -= 1  # самый последний элемент

    if order == 1:
        # апроксимация отрезками прямой
        first = (y[k + 1] - y[k]) / (x[k + 1] - x[k])
        # апроксимация многочленом второй степени
        first = first + \
                ((y[k + 2] - y[k + 1]) / (x[k + 2] - x[k + 1]) - (y[k + 1] - y[k]) / (x[k + 1] - x[k])) * \
                (2 * val - x[k] - x[k + 1]) / \
                (x[k + 2] - x[k])
        return first
    elif order == 2:
            # Для вычисления второй производной, необходимо использовать
            # интерполяционный многочлен, как минимум второй степени.
            second = 2 * ((y[k + 2] - y[k + 1]) / (x[k + 2] - x[k + 1]) - (y[k + 1] - y[k]) / (x[k + 1] - x[k])) \
                     / (x[k + 2] - x[k])
            return second
    else:
        return None


def main():
    x = [float(num) for num in input().split(' ')]
    y = [float(num) for num in input().split(' ')]
    point = float(input())


    polynom = newtons_polynom(x,y,len(x))

    # Первая производная
    print("Первая производная")
    res = simple_derivative(x, y, point, 1)
    print("Приближение по 2 точкам",res)
    res = newtons_derivative(polynom, point, 1 )
    print("Приближение м-м Ньютона", res)
    print()
    print("Вторая производная")
    polynom = newtons_polynom(x,y,len(x))
    res = newtons_derivative(polynom, point, 2)
    print("Приближение по 2 точкам",res)
    res = simple_derivative(x, y, point, 2)
    print("Приближение м-м Ньютона", res)

if __name__ == '__main__':
    main()
