DISK_SIZE = 70000000;
SIZE_NEEDED = 30000000;
FILE_MAX_SIZE = 100000;
FILE_PATH = "./input.txt"


class Node:
    def __init__(self, name: str, size: int, parent: "Node" = None):
        self.parent = parent
        self.name = name
        self.size = size
        self.files = []
        self.directories = []


def calculate_sizes(s):
    size = 0
    for x in s.files:
        size += x.size
    for x in s.directories:
        size += calculate_sizes(x)
    s.size = size
    return size


def get_part_one(s):
    total = 0
    for x in s.directories:
        if x.size <= FILE_MAX_SIZE:
            total += x.size
        total += get_part_one(x)
    return total


def get_part_two(s):
    sizes = []
    for x in s.directories:
        sizes.extend(get_part_two(x))

    sizes.append(s.size)
    if s.parent is None:
        sizes.sort(reverse=True)
        to_remove = min([size for size in sizes if size >= SIZE_NEEDED - (DISK_SIZE - sizes[0])])
        return to_remove
    else:
        return sizes


hierarchy = Node("/", 0)
root = hierarchy
current_directory = hierarchy


with open(FILE_PATH, "r") as file:
    content = file.read()

for line in content.splitlines():
    if line.startswith("$"):
        action = line.replace("$", "").strip().split(" ")
        if action[0] == "cd":
            if action[1] == "..":
                current_directory = current_directory.parent
            elif action[1] == "/":
                current_directory = root
            else:
                for x in current_directory.directories:
                    if x.name == action[1]:
                        current_directory = x
                        break

    elif line.startswith("dir"):
        dir_name = line.replace("dir ", "")
        current_directory.directories.append(
            Node(name=dir_name, size=0,parent=current_directory)
        )
    else:
        file_infos = line.split(" ")
        current_directory.files.append(
            Node(name=file_infos[1], size=int(file_infos[0]))
        )

calculate_sizes(hierarchy)
print("Part one : " + str(get_part_one(hierarchy)))
print("Part two : " + str(get_part_two(hierarchy)))
