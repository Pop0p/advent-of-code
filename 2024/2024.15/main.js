const fs = require("fs");
const inp = fs
  .readFileSync("./input.txt", "utf8")
  .replace(/\r\n/g, "\n")
  .split(/\n{2,}/)
  .map((block) => block.trim())
  .filter(Boolean);

const moves = inp[1].replace(/(\r\n|\n|\r)/g, "");

const movements = {
  "^": [0, -1],
  ">": [1, 0],
  "v": [0, 1],
  "<": [-1, 0],
};

function simulate(grid) {
  let sy = grid.findIndex((x) => x.includes("@"));
  let sx = grid[sy].indexOf("@");

  let dx, dy;
  for (const movement of moves) {
    [dx, dy] = movements[movement];
    if (move(grid, sx, sy, dx, dy)) {
      sx += dx;
      sy += dy;
    }
  }

  let total = 0;
  for (let y = 0; y < grid.length; y++) {
    for (let x = 0; x < grid[y].length; x++) {
      if (grid[y][x] == "O") total += 100 * y + x;
      if (grid[y][x] == "[") total += 100 * y + x;
    }
  }

  return total;
}

function move(grid, fx, fy, dx, dy, apply = true) {
  const tx = fx + dx;
  const ty = fy + dy;

  if (tx == 0 || ty == 0) return false;
  if (tx == grid[0].length || ty == grid.length) return false;

  const from = grid[fy][fx];
  const target = grid[ty][tx];

  switch (target) {
    case "#":
      return false;
    case ".":
      if (apply) {
        grid[ty][tx] = from;
        grid[fy][fx] = ".";
      }
      return true;
    case "O":
      if (move(grid, tx, ty, dx, dy)) {
        if (apply) {
          grid[ty][tx] = from;
          grid[fy][fx] = ".";
        }
        return true;
      }
      break;
    case "[":
    case "]":
      // If moving on the Y axis, we must check both side of our larger boxes.
      if (dy != 0) {
        const sideX = target == "]" ? tx - 1 : tx + 1;
        if (move(grid, tx, ty, dx, dy, false) && move(grid, sideX, ty, dx, dy, false)) {
          if (apply) {
            move(grid, tx, ty, dx, dy);
            move(grid, sideX, ty, dx, dy);
            grid[ty][tx] = from;
            grid[ty][sideX] = ".";
            grid[fy][fx] = ".";
          }
          return true;
        }
      } else {
        if (move(grid, tx, ty, dx, dy)) {
          if (apply) {
            grid[ty][tx] = from;
            grid[fy][fx] = ".";
          }
          return true;
        }
      }
      break;
  }
  return false;
}
console.log("part one :", simulate(inp[0].split("\n").map((x) => x.split(""))));
console.log(
  "part two :",
  simulate(
    inp[0]
      .replaceAll("#", "##")
      .replaceAll(".", "..")
      .replaceAll("O", "[]")
      .replaceAll("@", "@.")
      .split("\n")
      .map((x) => x.split(""))
  )
);
