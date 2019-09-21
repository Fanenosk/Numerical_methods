import math


def exact(x):
    return (1 + x) * math.e ** (-x ** 2)


def f(x, y, z):
    return z


def g(x, y, z):
    # return 2 * x * z / (x ** 2 + 1)
    return -4 * x * z - (4 * x ** 2 + 2) * y


def euler(x0, y0, z0, h, n):
    x, y, z = [x0], [y0], [z0]
    for k in range(n - 1):
        y.append(y[k] + h * f(x[k], y[k], z[k]))
        z.append(z[k] + h * g(x[k], y[k], z[k]))
        x.append(x[k] + h)
    return x, y, z


def adams(h, n, x, y, z):
    # метод Адамса
    # https://ru.wikipedia.org/wiki/%D0%9C%D0%B5%D1%82%D0%BE%D0%B4_%D0%90%D0%B4%D0%B0%D0%BC%D1%81%D0%B0
    for k in range(3, n - 1):
        del_y = 55 * f(x[k], y[k], z[k]) \
                - 59 * f(x[k - 1], y[k - 1], z[k - 1]) \
                + 37 * f(x[k - 2], y[k - 2], z[k - 2]) \
                - 9 * f(x[k - 3], y[k - 3], z[k - 3])
        del_z = 55 * g(x[k], y[k], z[k]) \
                - 59 * g(x[k - 1], y[k - 1], z[k - 1]) \
                + 37 * g(x[k - 2], y[k - 2], z[k - 2]) \
                - 9 * g(x[k - 3], y[k - 3], z[k - 3])
        y.append(y[k] + h / 24 * del_y)
        z.append(z[k] + h / 24 * del_z)
        x.append(x[k] + h)
    return x, y, z


def runge_kutt(x0, y0, z0, f, g, h, n):
    # Метод Рунге-Кутта произвольного порядка для системы двух ДУ
    p = 4
    a = [0, 1 / 2, 1 / 2, 1]
    c = [1 / 6, 1 / 3, 1 / 3, 1 / 6]
    b = [
        [],
        [1 / 2, ],
        [0, 1 / 2],
        [0, 0, 1 / 2],
    ]

    x, y, z = [x0], [y0], [z0]
    fault = [0.]  # погрешность
    K = [0.] * p
    L = [0.] * p
    for k in range(n - 1):
        xk = x[k]
        yk = y[k]
        zk = z[k]
        del_y, del_z = 0, 0
        for i in range(p):
            # вычисляем значения К и L
            summ_K = 0
            summ_L = 0
            for j in range(i):
                summ_K += b[i][j] * K[j]
                summ_L += b[i][j] * L[j]
            K[i] = h * f(xk + a[i] * h,
                         yk + summ_K,
                         zk + summ_L)

            L[i] = h * g(xk + a[i] * h,
                         yk + summ_K,
                         zk + summ_L)
            # вычисляем значение дельта
            del_y += c[i] * K[i]
            del_z += c[i] * L[i]
        y.append(y[k] + del_y)
        z.append(z[k] + del_z)
        x.append(xk + h)
        # контроль точности решения методом Рунге - Ромберга - Ричардсона.
        # if abs(K[0] - K[1]) > 0 and abs((K[1] - K[2]) / (K[0] - K[1])) > 0.1:
        #     # шаг следует уменьшить
        #     h /= 2
        if abs(K[0] - K[1]) > 0:
            fault.append(((K[1] - K[2]) / (K[0] - K[1])))
        else:
            fault.append(0.)
    return x, y, z, fault


def print_res(method, x, y, z, fault, exact, n):
    print(method)
    print("x", end=' ')
    for point in x:
        print("{:5f}".format(point), end=' ')
    print()
    print("y", end=' ')
    for point in y:
        print("{:5f}".format(point), end=' ')
    print()
    print("Промежуточные значения z = y' ")
    print("z", end=' ')
    for point in z:
        print("{:5f}".format(point), end=' ')
    print()
    if fault:
        print("Погрешность методом Рунге – Ромберга")
        print(" ", end=' ')
        for point in fault:
            print("{:5f}".format(point), end=' ')
        print()
    print("Сравнение с точным решением")
    print(" ", end=' ')
    for i in range(n):
        print("{:5f}".format(abs(exact(x[i]) - y[i])), end=' ')
    print()
    print()


def main():
    h = float(input())
    x_min, x_max = 0., 1.
    y = [1]
    z = [1]
    n = int(math.ceil((x_max - x_min) / h) + 1)

    method = "Решение методом Эйлера:"
    x, y, z = euler(x_min, y[0], z[0], h, n)
    print_res(method, x, y, z, [], exact, n)

    method = "Решение методом Рунге-Кутта:"
    x, y, z, fault = runge_kutt(x_min, y[0], z[0], f, g, h, n)
    print_res(method, x, y, z, fault, exact, n)

    x, y, z = adams(h, n, x[:4], y[:4], z[:4])

    method = "Решение методом Адамса:"
    print_res(method, x, y, z, [], exact, n)


if __name__ == '__main__':
    main()
