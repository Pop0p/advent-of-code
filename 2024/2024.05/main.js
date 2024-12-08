const fs = require("fs");
const file = fs
  .readFileSync("input.txt", "utf8")
  .trim()
  .split(/\r?\n/)
  .map((x) => x.trim());

const part_one = file.slice(0, file.indexOf("")).map((r) => r.split(/\|/).map(Number));
const part_two = file.slice(file.indexOf("") + 1).map((r) => r.split(/,/).map(Number));
const filtered_part_one = part_one.filter(([a, b]) => part_two.some((r) => r.includes(a) && r.includes(b)));
const numbers = filtered_part_one.reduce((acc, [before, after]) => {
  acc[before] = [...(acc[before] || []), after];
  return acc;
}, {});

let part_one_sum = 0;
let part_two_sum = 0;
let isSearchingInvalid = false;

for (let x = 0; x < part_two.length; x++) {
  const update = part_two[x];
  let isValid = true;
  let invalidIndex = -1;
  for (let y = 0; y < update.length; y++) {
    if (update.slice(y + 1).some((x) => !numbers[update[y]].includes(x))) {
      isValid = false;
      invalidIndex = y;
      break;
    }
  }

  if (isValid) {
    const midValue = update[Math.floor(update.length / 2)];
    isSearchingInvalid ? (part_two_sum += midValue) : (part_one_sum += midValue);
    isSearchingInvalid = false;
  } else {
    isSearchingInvalid = true;
    const element = update[invalidIndex];
    if (!numbers[element].length) update.push(update.splice(invalidIndex, 1)[0]);
    else {
      const position = update.findIndex((x) => !numbers[element].includes(x) && x != element && update.indexOf(x) > invalidIndex);
      update.splice(invalidIndex, 0, update.splice(position, 1)[0]);
    }
    x--;
  }
}

console.log(part_one_sum);
console.log(part_two_sum);
