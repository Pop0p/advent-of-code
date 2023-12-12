const fs = require('fs');

const directions = [[-1, -1], [-1, 0], [-1, 1], [0, -1], [0, 1], [1, -1], [1, 0], [1, 1]];

const content = fs.readFile("./input.txt", 'utf8', (error, content) => {
    const rows = content.split(/\r?\n/).map(r => r.split(""));
    const width = /\r?\n/.exec(content).index;
    const stars = {};
    let sum = 0;
    for (let y = 0; y < rows.length; y++) {
        const row = rows[y];
        for (let x = 0; x < row.length; x++) {
            const col = row[x];
            if (isNaN(col)) continue;

            const to_add = [];
            if (!directions.some(([dy, dx]) => rows[y + dy]?.[x + dx]?.match(/[^0-9.]+/) && (rows[y + dy][x + dx] == '*' && to_add.push(`${y + dy}:${x + dx}`), true)))
                continue

            let number = col;
            let jump = 0;
            for (let i = x - 1; i >= 0; i--) {
                if (!row[i] || isNaN(row[i]))
                    break;
                else
                    number = row[i] + number;
            }
            for (let i = x + 1; i < width; i++) {
                if (!row[i] || isNaN(row[i]))
                    break;
                else
                    number += row[i];
                jump += 1;
            }
            sum += number * 1;
            x += jump;
            to_add.forEach(x => (stars[x] = stars[x] || []).push(number));
        }
    }
    console.log(sum);
    console.log(Object.values(stars).reduce((acc, arr) => arr.length == 2 ? acc + arr.reduce((product, value) => product * value, 1) : acc, 0));
})