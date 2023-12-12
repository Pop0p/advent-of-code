use std::fs;

fn main() {
    let content = read_file();
    let calories = parse_file(content);
    println!("{}", calories.first().unwrap());
    println!("{}", calories[..3].iter().sum::<u32>());
}

fn read_file() -> String {
    const FILE_PATH: &str = "./input.txt";
    let content =
        fs::read_to_string(FILE_PATH).expect(&format!("Error when reading {}", FILE_PATH));

    content
}

fn parse_file(content: String) -> Vec<u32> {
    let calories: Vec<&str> = content.trim().split("\n\n").collect();
    let mut sum_calories: Vec<u32> = Vec::new();

    for c in calories {
        let mut sum: u32 = 0;
        let elfe_calories: Vec<&str> = c.split("\n").collect();
        for ec in elfe_calories {
            let calorie: u32 = ec
                .trim()
                .parse()
                .expect(&format!("Number {} is invalid", ec));
            sum += calorie;
        }
        sum_calories.push(sum);
    }
    sum_calories.sort_by(|a, b| b.cmp(a));

    return sum_calories;
}
