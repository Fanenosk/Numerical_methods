import matplotlib.pyplot as plt
import numpy as np
from lab1_2 import tridiagonal

n = int(input())
x = [float(num) for num in input().split(' ')]
f = [float(num) for num in input().split(' ')]
x_0 = float(input())


def cubic_spline(x, f):
    n = len(x)

    def h(i):
        return x[i] - x[i - 1]

    a, b, c, d = [], [], [], []
    # b- главная диагональ
    # a - над ней
    # b - под ней
    # d - столбец свободных членов
    a = [0.] + [h(i - 1) for i in range(3, n)]
    b = [2 * (h(i - 1) + h(i)) for i in range(2, n)]
    c = [h(i) for i in range(2, n - 1)] + [0.]
    d = [3 * ((f[i] - f[i - 1]) / h(i) - ((f[i - 1] - f[i - 2]) / h(i - 1)))
         for i in range(2, n)]

    C = [0, 0] + tridiagonal(a, b, c, d, n - 2)
    A = [0] + [f[i] for i in range(n - 1)]
    B = [0]
    for i in range(1, n - 1):
        B.append((f[i] - f[i - 1]) / h(i) - 1 / 3 * h(i) * (C[i + 1] + 2 * C[i]))
    B.append((f[n - 1] - f[n - 2]) / h(n - 1) - 2 / 3 * h(n - 1) * C[n - 1])
    D = [0] + [(C[i + 1] - C[i]) / (3 * h(i)) for i in range(1, n - 1)]
    D.append(-C[n - 1] / 3 * h(n - 1))

    S = []
    for i in range(1, n):
        S.append(
            [x[i - 1], A[i], B[i], C[i], D[i]])

    return S


def spline_print(S, x):
    spline_template = "{:5.2f} + {:5.2f}(x - {:5.2f}) + {:5.2f}(x - {:5.2f})^2 + {:5.2f}(x - {:5.2f})^3"
    for i in range(1, len(x)):
        print("[{:1.1f}, {:5.1f}]"
              .format(x[i - 1], x[i]), end=' : ')
        print(spline_template
            .format(
            S[i - 1][1],
            S[i - 1][2],
            S[i - 1][0],
            S[i - 1][3],
            S[i - 1][0],
            S[i - 1][4],
            S[i - 1][0]))


def calc_spline(spline, x, val):
    fields = [(x[i - 1], x[i]) for i in range(1, len(x))]
    k = 0
    for i, x in enumerate(fields):
        if x[1] >= val >= x[0]:
            k = i
            break

    def calc(s, x):
        return s[1] + s[2] * (x - s[0]) + s[3] * ((x - s[0]) ** 2) + s[4] * ((x - s[0]) ** 3)

    return calc(spline[k], val)


spline = cubic_spline(x, f)
spline_print(spline, x)
print("Значение в точке {} согласно простроенному сплайну: {}". \
      format(x_0, calc_spline(spline, x, x_0)))

y = [calc_spline(spline, x, val) for val in x]

X = np.arange(x[0], x[-1], 0.01)
Y = [calc_spline(spline, x, val) for val in X]

fig, ax = plt.subplots()
ax.plot(X, Y, color='k')
f[-1] += 0.04
ax.plot(x, f, 'bo')

ax.grid()
plt.show()
