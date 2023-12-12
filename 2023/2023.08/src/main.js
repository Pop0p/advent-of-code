const fs = require("fs");
let lines = fs.readFileSync("./input.txt", "utf8").toString().split(/\r?\n/);
const instructions = lines[0].split("");
let nodes = lines.slice(2);
let starting_nodes_p1 = [];
let starting_nodes_p2 = [];
const map = {};

for (let i = 0; i < nodes.length; i++) {
  const node_row = nodes[i].split(" = ");
  const node_name = node_row[0];
  const node_directions = node_row[1].replace(/[^0-9A-Z\s]/g, "").split(" ");
  map[node_name] = [node_directions[0], node_directions[1]];
  if (node_name.match(/[A-Z0-9]{2}A/)) starting_nodes_p2.push(node_name);
  if (node_name.match(/AAA/)) starting_nodes_p1.push(node_name);
}

function GCD(a, b) {
  while (b !== 0) {
    let t = b;
    b = a % b;
    a = t;
  }
  return a;
}
function getCycle(node, nodes, instructions, end) {
  let visited = new Set();
  let step = 0;
  while (true) {
    visited.add(node);
    let direction = instructions[step % instructions.length] === "L" ? 0 : 1; // Choose direction based on instructions
    node = nodes[node][direction];
    step++;

    if (node.endsWith(end)) break;
  }
  return step;
}

// P1
let result_one = starting_nodes_p1.map((node) => getCycle(node, map, instructions, "ZZZ"))[0];
console.log(result_one);
// P2
let cycleLengths = starting_nodes_p2.map((node) => getCycle(node, map, instructions, "Z"));
let overallLCM = [5, 12, 100].reduce((a, b) => (a * b) / GCD(a, b));
console.log(overallLCM);
