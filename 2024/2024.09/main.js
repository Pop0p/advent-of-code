const fs = require("fs");
const disk = fs.readFileSync("input.txt", "utf8").split("").map(Number);

const files_part = new Map();
const files_map = [];
let spaces = [];
let size = 0;
for (let i = 0; i < disk.length; i++) {
  const block = disk[i];
  const range = Array.from({ length: block }, (_, idx) => size + idx);
  const index = Math.floor(i / 2);
  if (i % 2 == 0) {
    files_part.set(index, range);
    files_map.push(index);
  } else spaces.push(range);

  size += block;
}

function part_one(parts) {
  const flat_spaces = spaces.flat();
  let leftmost_free_space_index = 0;
  for (let i = files_map.length - 1; i > 0; i--) {
    let file_blocks = parts.get(i);
    for (let x = file_blocks.length - 1; x >= 0; x--) {
      const position = file_blocks[x];
      if (flat_spaces[leftmost_free_space_index] >= position) break;
      file_blocks[x] = flat_spaces[leftmost_free_space_index];
      leftmost_free_space_index += 1;
      if (leftmost_free_space_index == flat_spaces.length - 1) break;
    }
  }

  console.log("part one :", get_result(parts));
}
function part_two(parts) {
  for (let i = files_map.length - 1; i > 0; i--) {
    let file_blocks = parts.get(i);

    const first_big_enough_spaces_block = spaces.findIndex((x, i) => x.length >= file_blocks.length && x[x.length - 1] < file_blocks[0]);
    if (first_big_enough_spaces_block == -1) continue;

    const spaces_block = spaces[first_big_enough_spaces_block];
    const new_positions = spaces_block.splice(0, file_blocks.length);
    spaces.push(file_blocks);

    parts.set(i, new_positions);
  }
  console.log("part two :", get_result(parts));
}
function get_result(files_part) {
  let total = 0;
  for (let i = 0; i < files_map.length; i++) {
    const file_index = files_map[i];
    total += file_index * files_part.get(file_index).reduce((acc, curr) => (acc += curr), 0);
  }
  return total;
}

part_one(structuredClone(files_part));
part_two(structuredClone(files_part));
