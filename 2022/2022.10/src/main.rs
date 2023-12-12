use std::fs;

const FILE_PATH: &str = "./input.txt";

fn main() {
    let content =
        fs::read_to_string(FILE_PATH).expect(&format!("Error when reading {}", FILE_PATH));
    solve(&content);
}

fn solve(content: &str) {
    let (steps, mut scores, mut screen): (Vec<i16>, Vec<i32>, Vec<&str>) =
        (vec![20, 60, 100, 140, 180, 220], Vec::new(), Vec::new());
    let (mut register, mut cycle, mut step_index): (i16, i16, usize) = (1, 0, 0);
    for line in content.lines() {
        let c = if line.eq("noop") { 1 } else { 2 };
        for _ in 0..c {
            screen.push(if (register - (cycle % 40)).abs() <= 1 { "#" } else { "." });
            cycle += 1;
            if cycle == steps[step_index] {
                scores.push((steps[step_index] * register).into());
                if step_index < 5 {
                    step_index += 1;
                }
            }
        }
        if c == 2 {
            let params: Vec<&str> = line.split(" ").collect();
            register += params[1].parse::<i16>().unwrap();
        }
    }
    // Part one
    println!("{:?}", scores.iter().sum::<i32>());
    // Part two
    for chunk in screen.chunks(40) {
        println!(
            "{}",
            chunk.iter().map(|s| s.to_string()).collect::<String>()
        );
    }
}
