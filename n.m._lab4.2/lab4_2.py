import math
import numpy as np
from lab4_1 import runge_kutt
from lab1_2 import tridiagonal
import lab1_1
from matrix_class import *


def runge_romberg(h1, h2, y1, y2, n=2):
    return abs((y1 - y2) / ((h2 / h1) ** n - 1.0))


def exact(x):
    return -math.tan(x)


def f_1(x, y, z):
    return z


def g_1(x, y, z):
    return 2*(1+((math.tan(x)) ** 2)) * y


def p(x):
    return 0.


def q(x):
    return -2*(1+((math.tan(x)) ** 2))


def f(x):
    return 0.


def shooting(xa, xb, ya, yb, h, f, g):
    n = int(math.ceil((xb - xa) / h))
    eta = [1, 0.8]  # некоторое значение тангенса угла наклона касательной
    # к решению в точке a из [a,b]

    eps = 0.000001
    F = []
    for et in eta:
        # решаем задачу коши методом Рунге - Кутта
        x, y, z, _ = runge_kutt(xa, ya, et, f, g, h, n)
        F.append(y[-1] - yb)
    k = 2
    while True:
        # вычисляем новую 'эта'
        eta.append(
            eta[k - 1] - (eta[k - 1] - eta[k - 2]) /
            (F[k - 1] - F[k - 2]) * F[k - 1]
        )

        x, y, z, _ = runge_kutt(xa, ya, eta[k], f, g, h, n)
        F.append(y[-1] - yb)
        # проверяем удовлетворение условия
        if abs(F[k]) < eps:
            break
        k += 1
    return x, y, eta, F


def finite_diff(x, ya, yb, h, n):
    # Вслучае использования граничных условий второго и третьего рода аппроксимация
    # производных проводится с помощью односторонних разностей первого и второго порядков.
    # в случае с первым порядком -- ситстема будет трёхдиагональна


    # составляем СЛАУ с неизвестными y[k]
    last = n - 1  # последний элемент
    a = [0.]
    b = [-2 + (h ** 2) * q(x[0])]
    c = [1 + p(x[0]) * h / 2]
    d = [((h ** 2) * f(x[0])) - (1 - p(x[0]) * h / 2)*ya]
    for k in range(1, last):
        a += [1 - p(x[k]) * h / 2]
        b += [-2 + (h ** 2) * q(x[k])]
        c += [1 + p(x[k]) * h / 2]
        d += [(h ** 2) * f(x[k])]
    a += [1 - p(x[last]) * h / 2]
    b += [-2 + (h ** 2) * q(x[last])]
    c += [0.]
    d += [((h ** 2) * f(x[last])) - (1 + p(x[last]) * h / 2)*yb]

    y = tridiagonal(a, b, c, d, n)

    return y


def main():
    h = 0.1
    xa, xb = 0, math.pi/6
    ya = 0
    yb = -(3 ** 0.5) / 3
    n = int(math.ceil((xb - xa) / h))

    print("Конечно-разностный метод")
    x = [xa + i * h for i in range(n)]
    y = finite_diff(x, ya, yb, h, n)
    print('x        ', end='')
    for elem in x:
        print("{:5.5f}".format(elem), end=' ')
    print()
    print('y        ', end='')
    for elem in y:
        print("{:5.5f}".format(elem), end=' ')
    print()
    print('Погрешн. ', end='')
    for i in range(n):
        val = abs(exact(x[i]) - y[i])
        print("{:5.5f}".format(val), end=' ')
    print()
    print('Погр. РР ', end='')
    x2 = [xa + i * h for i in range(n * 2)]
    y2 = finite_diff(x2, ya, yb, h / 2, n * 2)
    for i in range(n):
        print("{:5.5f}".format(runge_romberg(h, h / 2, y[i], y2[i * 2]), 3), end=' ')

    print()
    ya, yb = exact(xa), exact(xb)
    # ya, yb = 1,2
    x, y, eta, F = shooting(xa, xb, ya, yb, h, f_1, g_1)
    print()
    print("Метод стрельбы")
    print('x        ', end='')
    for elem in x:
        print("{:5.5f}".format(elem), end=' ')
    print()
    print('y        ', end='')
    for elem in y:
        print("{:5.5f}".format(elem), end=' ')
    print()
    print('Погрешн. ', end='')
    for i in range(n):
        val = abs(exact(x[i]) - y[i])
        print("{:5.5f}".format(val), end=' ')
    print()
    print('Погр. РР ', end='')
    ya, yb = exact(xa), exact(xb)

    x2, y2, eta2, F2 = shooting(xa, xb, ya, yb, h / 2, f_1, g_1)
    for i in range(n):
        print("{:5.5f}".format(runge_romberg(h, h / 2, y[i], y2[i * 2])), end=' ')

    print()
    print("  эта     f".format(xb, yb))
    for i in range(len(eta)):
        print("{:+5.5f} {:5.5f}".format(eta[i], F[i] + yb))


if __name__ == '__main__':
    main()
