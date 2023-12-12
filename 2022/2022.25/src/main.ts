import { input } from "./inputs";



function solve() {
    let inputs: string[] = input.trim().replace(/[\n]+/g, ",").split(",");
    let total = 0;
    for (let i = 0; i < inputs.length; i++) {
        let input = inputs[i];
        let size = input.length - 1;
        for (let x = size; x >= 0; x--) {
            let char = input[x] == "=" ? "-2" : input[x] == "-" ? "-1" : input[x];
            total += parseInt(char) * (5 ** (size - x));
        }
    }
    console.log("Part 1 : " + total);

    let snafu = []
    while (total != 0) {
        let val = (total % 5).toString();
        let r = (val == "4" ? "-" : val == "3" ? "=" : val);
        /* In Snafu we have to add a new left column sooner than in base 5 (5 - 2).
         * | 10b | 05b |snafu|
         * ----------------
         * |  1  |  1  |  1  |
         * |  3  |  3  |  1= |
         * |  4  |  4  |  1- |
         * |  5  |  10 |  10 |
         * |  8  |  13 |  2= |
         */
        total = Math.floor((total + 2) / 5);
        snafu.push(r);
    }
    console.log("Part 2 : " + snafu.reverse().join(""));


}
solve();