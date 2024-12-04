const fs = require("fs");
let lines = fs.readFileSync("./input.txt", "utf8").trim();
const grid = lines.split("\n").map((line) => line.replace("\r", "").split(""));

let part_one = 0;
let part_two = 0;
const directions_part_one = [
  { x: -1, y: 0 },
  { x: -1, y: 1 },
  { x: 0, y: 1 },
  { x: 1, y: 1 },
  { x: 1, y: 0 },
  { x: 1, y: -1 },
  { x: 0, y: -1 },
  { x: -1, y: -1 },
];

for (let y = 0; y < grid.length; y++) {
  const row = grid[y];
  for (let x = 0; x < row.length; x++) {
    if (row[x] == "X") {
      for (let direction of directions_part_one) {
        if (scan_part_one("XMAS", x, y, direction.x, direction.y, 1))
          part_one += 1;
      }
    }
    if (row[x] == "A") {
      if (scan_part_two(x, y)) {
        part_two += 1;
      }
    }
  }
}

function scan_part_one(word, from_x, from_y, to_x, to_y, current_word_index) {
  if (isOutOfBounds(from_x, from_y, to_x, to_y, grid[0].length, grid.length))
    return false;
  if (grid[from_y + to_y][from_x + to_x] != word[current_word_index])
    return false;
  if (current_word_index == word.length - 1) return true;
  return scan_part_one(
    word,
    from_x + to_x,
    from_y + to_y,
    to_x,
    to_y,
    current_word_index + 1
  );
}

function scan_part_two(from_x, from_y) {
  if (isOutOfBounds(from_x, from_y, 1, 1, grid[0].length, grid.length))
    return false;
  if (isOutOfBounds(from_x, from_y, -1, 1, grid[0].length, grid.length))
    return false;
  if (isOutOfBounds(from_x, from_y, -1, -1, grid[0].length, grid.length))
    return false;
  if (isOutOfBounds(from_x, from_y, 1, -1, grid[0].length, grid.length))
    return false;

  const NW = grid[from_y - 1][from_x - 1];
  const NE = grid[from_y - 1][from_x + 1];
  const SW = grid[from_y + 1][from_x - 1];
  const SE = grid[from_y + 1][from_x + 1];

  if (NW + SE != "MS" && NW + SE != "SM") return false;
  if (NE + SW != "MS" && NE + SW != "SM") return false;

  return true;
}

function isOutOfBounds(from_x, from_y, to_x, to_y, width, height) {
  if (from_x == 0 && to_x == -1) return true;
  if (from_y == 0 && to_y == -1) return true;
  if (from_x == width - 1 && to_x == 1) return true;
  if (from_y == height - 1 && to_y == 1) return true;
}

console.log(`part one : ${part_one}, part two : ${part_two}`);
