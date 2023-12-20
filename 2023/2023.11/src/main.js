const fs = require("fs");


fs.readFile("./input.txt", "utf8", (e, c) => {
    const rows = c.split(/\r?\n/).map(r => r.split(""));
    const empty_cols = rows[0].map((_, i) => i).filter(i => rows.every(r => r[i] !== "#"));
    const empty_rows = rows.reduce((acc, row, i) => !row.includes("#") ? [...acc, i] : acc, []);
    const galaxies = rows.flatMap((row, y) => row.map((cell, x) => cell === "#" ? [x, y] : null).filter(coord => coord));

    let sum_part_one = 0;
    let sum_part_two = 0;
    for (let i = 0; i < galaxies.length; i++) {
        for (let z = 0 + i; z < galaxies.length; z++) {
            sum_part_one += getDistance(galaxies[i], [galaxies[z][0], galaxies[z][1]], empty_cols, empty_rows, 1);
            sum_part_two += getDistance(galaxies[i], [galaxies[z][0], galaxies[z][1]], empty_cols, empty_rows, 999999);
        }
    };
    console.log(sum_part_one);
    console.log(sum_part_two);
})

function getDistance(a, b, empty_cols, empty_rows, expansion) {
    const highest_x = Math.max(a[0], b[0]);
    const highest_y = Math.max(a[1], b[1]);
    const ecc = empty_cols.filter(ec => ec > Math.min(a[0], b[0]) && ec < highest_x).length * expansion;
    const erc = empty_rows.filter(ec => ec > Math.min(a[1], b[1]) && ec < highest_y).length * expansion;
    b[0] += (highest_x == a[0] ? -ecc : ecc);
    b[1] += (highest_y == a[1] ? -erc : erc);
    return Math.abs(b[0] - a[0]) + Math.abs(b[1] - a[1]);
}
