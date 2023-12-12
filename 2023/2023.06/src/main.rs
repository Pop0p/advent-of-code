use std::fs;

fn main() {
    let content = fs::read_to_string("./input.txt").expect("Error when reading input file");
    let lines: Vec<&str> = content.split('\n').collect();
    let times_one: Vec<i64> = lines[0].replace("Time:", "").split_whitespace().filter_map(|s| s.parse().ok()).collect();
    let distances_one : Vec<i64> = lines[1].replace("Distance:", "").split_whitespace().filter_map(|s| s.parse().ok()).collect();
    let pairs_one : Vec<(i64, i64)> = times_one.iter().zip(distances_one.iter()).map(|(&t, &d) | (t, d)).collect();
    do_work(pairs_one);

    let times_two: i64 = lines[0].replace("Time:", "").split_whitespace().collect::<String>().parse::<i64>().unwrap_or(0);
    let distances_two : i64 = lines[1].replace("Distance:", "").split_whitespace().collect::<String>().parse::<i64>().unwrap_or(0);
    let pairs_two : Vec<(i64, i64)> = vec![(times_two, distances_two)];
    do_work(pairs_two);
}

fn do_work(pairs : Vec<(i64, i64)>) {
    let mut total = 1;
    for pair in pairs {
        let  mut i : i64 = 0;
        for number in 0..pair.0 {
            if number * (pair.0 - number) > pair.1 {
                break;
            }
            i+=1;
        }
        total *= pair.0 - (i*2) + 1;
    }
    println!("Total : {}", total);
}
