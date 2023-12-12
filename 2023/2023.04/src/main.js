const fs = require("fs");

const content = fs.readFile("./input.txt", "utf8", (error, content) => {
  const rows = content.split(/\r?\n/).map((r) => r.split(":")[1]);
  const scs = Object.fromEntries(rows.map((_, i) => [i, 1]));
  let sum = 0;
  for (let i = 0; i < rows.length; i++) {
    const [winners, picked] = rows[i].split("|").map((r) => r.split(" ").filter(Boolean));
    const inters = winners.filter((num) => picked.includes(num));
    if (inters.length > 0) {
      sum += Math.max(Math.pow(2, inters.length - 1), 1);
      for (let z = 0; z < inters.length; z++)
        scs[i + z + 1] += 1 * scs[i];
    }
  }
  console.log(sum);
  console.log(Object.values(scs).reduce((acc, val) => acc + val, 0));
});
