const fs = require("fs");
let lines = fs.readFileSync("./input.txt", "utf8").trim().split(/\r?\n/).map(line => line.split(" ").map(Number));


let one = 0;
let two = 0;
for (const line of lines) {
    const differences = calculateDifferences(line);
    if (isReportSafe(differences))
        one += 1;
    else
        for (let i = 0; i < line.length; i++) {
            const copy = [...line];
            copy.splice(i, 1);
            const lineOfDifferences = calculateDifferences(copy);
            if (isReportSafe(lineOfDifferences)) {
                two += 1;
                break;
            }
        }
}
console.log(`Part one : ${one}, part two : ${one + two}`);
function calculateDifferences(line) {
    return line.slice(0, -1).map((value, i) => value - line[i + 1]);
}
function isReportSafe(computedReport) {
    const isIncreasingOrDecreasing = computedReport.every(diff => Math.sign(diff) === -1) || computedReport.every(diff => Math.sign(diff) === 1);
    const everyAdjacentLevelDifferenceIsInBounds = computedReport.every(diff => Math.abs(diff) >= 1 && Math.abs(diff) <= 3);
    return isIncreasingOrDecreasing && everyAdjacentLevelDifferenceIsInBounds
}


