import { input } from "./inputs";
type ValueOrArray<T> = T | Array<ValueOrArray<T>>;


function solve() {
    let s = 0;
    let str = input.replace(/\n\n/g, "\n").split("\n");
    let chunks = [];
    const chunkSize = 2;
    for (let i = 0; i < str.length; i += chunkSize) {
        const chunk = str.slice(i, i + chunkSize);
        chunks.push(chunk);
    }
    // Part one
    for (let i = 0; i < chunks.length; i++) {
        let a = JSON.parse(chunks[i][0])
        let b = JSON.parse(chunks[i][1])
        let r = compare(a, b);
        if (r == -1)
            s += (i+1);
    }
    console.log("Part one : " + s);
    // Part two
    let decoder_keys = [ [ [ 2 ]  ], [ [ 6 ] ] ];
    let tt = str.map(x => JSON.parse(x)).concat(decoder_keys).sort(compare);
    console.log("Part two : " + (tt.indexOf(decoder_keys[0])+1) * (tt.indexOf(decoder_keys[1])+1));
}
function compare(a: ValueOrArray<number>, b: ValueOrArray<number>): number {
    if (typeof a === 'number' && typeof b === 'number')
        return a < b ? -1 : a > b ? 1 : 0
    else if (typeof a === 'number')
        return compare([a], b);
    else if (typeof b === 'number')
        return compare(a, [b]);

    for (let i = 0; i < a.length; i++) {
        if (b.length == i)
            return 1;
        let r = compare(a[i], b[i]);
        if (r != 0)
            return r;
    }

    let m = a.length - b.length;
    return m < 0 ? -1 : m > 0 ? 1 : 0;
}

solve();