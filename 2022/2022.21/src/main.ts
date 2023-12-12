import { input } from "./inputs";


// WORKS ON MY MACHINE ... somehow .. :)

function solve() {
    let inputs: string[] = input.trim().replace(/[\n]+/g, ",").split(",");
    inputs.splice(0, 0, inputs.splice(inputs.findIndex(x => x.includes("root:")), 1)[0]);
    let riddles: { [key: string]: string[] } = {};
    for (let i = 0; i < inputs.length; i++) {
        let monkey: string = inputs[i].split(":")[0];
        let job: string[] = inputs[i].split(": ")[1].split(" ");
        riddles[monkey] = job;
    }

    // Part one
    let copy = JSON.parse(JSON.stringify(riddles));
    console.log("Part one is " + do_solve(copy["root"], copy));

    // Part two
    copy = JSON.parse(JSON.stringify(riddles));
    copy["root"][1] = "-";
    copy["root"][0] = "0";
    let target_value = Math.abs(do_solve(copy["root"], copy));

    riddles["root"][1] = "-";
    riddles["root"][2] = "0";

    let result = 0;
    let valtest = 10;
    let granularite = 10000000000;
    let last = "+";
    while (result != target_value) {
        copy = JSON.parse(JSON.stringify(riddles));
        copy["humn"][0] = valtest;
        result = Math.trunc(Math.abs(do_solve(copy["root"], copy)));
        if (result > target_value) {
            valtest += Math.round(granularite);
            if (last != "-")
                Math.max(granularite /= 2, 1);
            last = "-";
        }
        else {
            valtest -= Math.round(granularite);
            if (last != "+")
                Math.max(granularite /= 2, 1);
            last = "+";
        }
    }
    console.log("Part two is " + Math.abs(valtest));
}

function do_solve(riddle: string[], riddles: { [key: string]: string[] }) {
    if (riddle.length == 1)
        return riddle[0];

    riddle[0] = isNaN(Number(riddle[0])) ? do_solve(riddles[riddle[0]], riddles) : riddle[0];
    riddle[2] = isNaN(Number(riddle[2])) ? do_solve(riddles[riddle[2]], riddles) : riddle[2];
    return eval(riddle.join(" "));
}



solve();