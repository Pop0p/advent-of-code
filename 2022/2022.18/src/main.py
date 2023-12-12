import queue

FILE_PATH = "./input.txt"
cubes = []


with open(FILE_PATH, "r") as file:
    content = file.read()
    for line in content.splitlines():
        coord = line.split(",")
        cubes.append((int(coord[0]), int(coord[1]), int(coord[2])))

# Part two : flood fill
water = []
q = queue.Queue()
min_pos = [min(t[i] for t in cubes) - 1 for i in range(3)]
max_pos = [max(t[i] for t in cubes) + 1 for i in range(3)]
q.put(min_pos)
while not q.empty():
    w = q.get()
    if w in water:
        continue
    water.append(w)
    neighbours = [
        (w[0] + 1, w[1], w[2]),
        (w[0] - 1, w[1], w[2]),
        (w[0], w[1] + 1, w[2]),
        (w[0], w[1] - 1, w[2]),
        (w[0], w[1], w[2] + 1),
        (w[0], w[1], w[2] - 1),
    ]
    for n in neighbours:
        if (
            n not in cubes
            and n[0] >= min_pos[0]
            and n[0] <= max_pos[0]
            and n[1] >= min_pos[1]
            and n[1] <= max_pos[1]
            and n[2] >= min_pos[2]
            and n[2] <= max_pos[2]
        ):
            q.put(n)

# Part one & two
total_part_one = 0
total_part_two = 0
for cube in cubes:
    neighbours = [
        (cube[0] + 1, cube[1], cube[2]),
        (cube[0] - 1, cube[1], cube[2]),
        (cube[0], cube[1] + 1, cube[2]),
        (cube[0], cube[1] - 1, cube[2]),
        (cube[0], cube[1], cube[2] + 1),
        (cube[0], cube[1], cube[2] - 1),
    ]
    for n in neighbours:
        if n not in cubes:
            total_part_one += 1
            if n not in water:
                total_part_two += 1

print(total_part_one)
print(total_part_one - total_part_two)
