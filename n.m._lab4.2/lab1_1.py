
def swap_lines(matrix, i, k):
    matrix[i], matrix[k] = matrix[k].copy(), matrix[i].copy()

def permute(A):
    n = len(A)
    p = []
    m = [row.copy() for row in A]
    for j in range(n - 1):
        tmp = [(m[i][j], i) for i in range(j, n)]
        idx = max(tmp, key=lambda x: abs(x[0]))[1]
        if idx != j:
            p.append((j, idx))
            swap_lines(m, j, idx)
    return p, m


def decompose_LU( A):
    size = len(A[0])
    L = [[0.0 for _ in range(size)] for _ in range(size)]

    p, U = permute(A)

    for i in range(size):
        L[i][i] = 1
        for j in range(i + 1, size):
            L[j][i] = U[j][i] / U[i][i]
            for k in range(i + 1, size):
                U[j][k] -= L[j][i] * U[i][k]

    for i in range(1, size):
        for j in range(i):
            U[i][j] = 0.0

    return L, U, p

def create_p_matrix(matrix, p):
    p_m = matrix.copy()

    for i, j in p:
        p_m[i], p_m[j] = p_m[j], p_m[i]
    return p_m

def solve_LU(L, U, B, p):
    size = len(L[0])
    Z = [0 for _ in range(size)]
    X = [0 for _ in range(size)]
    Bp = create_p_matrix(B, p)
    for i in range(size):
        Z[i] = Bp[i] - sum(L[i][j] * Z[j] for j in range(i))

    for i in reversed(range(size)):
        z = sum(U[i][j] * X[j] for j in range(i + 1, size))
        X[i] = (Z[i] - z) / U[i][i]
    return X
