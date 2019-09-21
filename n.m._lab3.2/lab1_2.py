def tridiagonal(a, b, c, d, n):
    P = [0.] * n
    Q = [0.] * n

    P[0] = -c[0] / b[0]
    Q[0] = d[0] / b[0]

    for i in range(1, n):
        P[i] = -c[i] / (a[i] * P[i - 1] + b[i])
        Q[i] = (d[i] - a[i] * Q[i - 1]) / (a[i] * P[i - 1] + b[i])

    x = [0.] * n
    # последний элемент
    x[n - 1] = Q[n - 1]
    for i in range(n - 1, 0, -1):
        x[i - 1] = P[i - 1] * x[i] + Q[i - 1]
    return x


def main():
    c = [8, 4, 5, -7, 0]
    b = [15, -15, 11, 16, 8]  # главная диагональ
    a = [0, 2, 4, -3, 3]
    d = [92, -84, -77, 15, -11]  # свободные члены

    n = 5
    x = tridiagonal(a, b, c, d, n)
    print("Метод прогонки.")
    print("Значения х:", x)


if __name__ == '__main__':
    main()
