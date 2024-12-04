const fs = require("fs");
let lines = fs.readFileSync("./input.txt", "utf8").trim();
let total = 0;
const grid = lines.split("\n").map((line) => line.replace("\r", "").split(""));
const directions = [
  { x: -1, y: 0 },
  { x: -1, y: 1 },
  { x: 0, y: 1 },
  { x: 1, y: 1 },
  { x: 1, y: 0 },
  { x: 1, y: -1 },
  { x: 0, y: -1 },
  { x: -1, y: -1 },
];
const word = "XMAS";

for (let y = 0; y < grid.length; y++) {
  const row = grid[y];
  for (let x = 0; x < row.length; x++) {
    const char = row[x];
    if (char != "X") continue;
    scanForWord(x, y, 1);
  }
}

function scanForWord(x, y, current_index) {
  let found = true;
  for (let direction of directions) {
    if (x == 0 && direction.x == -1) continue;
    if (x == grid[0].length - 1 && direction.x == 1) continue;
    if (y == 0 && direction.y == -1) continue;
    if (y == grid.length - 1 && direction.y == 1) continue;
    const char = grid[y + direction.y][x + direction.x];
    const searched_char = word[current_index];
    if (char == searched_char) {
      if (current_index == word.length - 1) {
        return true;
      }
      const result = scanForWord(x + direction.x, y + direction.y, current_index + 1);
      if (result)
        return true
    } else {
      found = false;
    }
  }
  if (!found) return false;
}
console.log(total);
