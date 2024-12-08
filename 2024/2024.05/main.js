const fs = require("fs");
const file = fs.readFileSync("input.txt", "utf8").trim().split(/\r?\n/).map(x => x.trim());

const part_one = file.slice(0, file.indexOf(""));
const part_two = file.slice(file.indexOf("") + 1);
console.log(part_two)
const numbers = [...new Set(part_two.flatMap(r => r.split(/[\|,]/).map(Number)))];

console.log(part_one);
console.log(part_two);

console.log(numbers);



