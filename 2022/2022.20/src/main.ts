import { input } from "./inputs";


// Not very fast and very dirty
function solve(part_two: boolean) {
    let arr : string[];
    if (part_two)
        arr = input.replace(/\n/g, ",").split(",").map(x => (Number(x) * 811589153).toString());
    else
        arr = input.replace(/\n/g, ",").split(",");

    let zero = arr.findIndex(x => x == "0");
    let numbers: Array<string> = arr.map((l, i) => l += `_${i}`);
    let new_numbers = [...numbers];
    for (let x = 0; x < (part_two ? 10 : 1); x++) {
        for (let i = 0; i < numbers.length; i++) {
            let current_position = new_numbers.indexOf(new_numbers.find(x => x == numbers[i]));
            let value = Number(new_numbers[current_position].replace(/_[\d]+/g, ""));
            let new_position = (current_position + value) % (numbers.length - 1);
            if (new_position == 0 && value < 0)
                new_position = numbers.length - 1;
            new_numbers.splice(new_position, 0, new_numbers.splice(current_position, 1)[0]);
        }
    }
    let zero_index = new_numbers.findIndex(x => x == numbers[zero])
    let a = Number(new_numbers[(zero_index + 1000) % numbers.length].replace(/_[\d]+/g, ""))
    let b = Number(new_numbers[(zero_index + 2000) % numbers.length].replace(/_[\d]+/g, ""))
    let c = Number(new_numbers[(zero_index + 3000) % numbers.length].replace(/_[\d]+/g, ""))

    console.log(a+b+c)
}


solve(false);
solve(true);