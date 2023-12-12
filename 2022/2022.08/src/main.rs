use std::fs;

const FILE_PATH: &str = "./input.txt";

fn main() {
    let content =
        fs::read_to_string(FILE_PATH).expect(&format!("Error when reading {}", FILE_PATH));
    let elements: Vec<Vec<u8>> = content
        .lines()
        .into_iter()
        .map(|r| r.chars().map(|c| c.to_digit(10).unwrap() as u8).collect())
        .collect();

    let mut visibles = 0;
    let mut highest = 0;
    for (row_index, row) in elements.iter().enumerate() {
        for (col_index, _col) in row.iter().enumerate() {
          let v : Node = do_tree(&elements, col_index as usize, row_index as usize);
          if v.score >= highest {
            highest = v.score;
          }
          if v.visible {
            visibles += 1;
          }
        }

    }

    println!("{}", visibles);
    println!("{}", highest);
}

struct Node {
    visible: bool,
    score: u64,
}

fn do_tree(trees: &Vec<Vec<u8>>, x: usize, y: usize) -> Node{
    let left_part: Vec<u8> = trees[y][0..x].iter().rev().map(|&e| e).collect();
    let right_part: Vec<u8> = trees[y][x + 1..].iter().map(|&e| e).collect();
    let top_part: Vec<u8> = trees[0..y]
        .iter()
        .rev()
        .enumerate()
        .map(|(_, e)| e[x])
        .collect();
    let bot_part: Vec<u8> = trees[y + 1..]
        .iter()
        .enumerate()
        .map(|(_, e)| e[x])
        .collect();
     
    // part one
    let vleft = get_visible(&left_part, trees[y][x]);
    let vright = get_visible(&right_part, trees[y][x]);
    let vtop = get_visible(&top_part, trees[y][x]);
    let vbot = get_visible(&bot_part, trees[y][x]);
    
    // part_two
    let lscore = get_score(&left_part, trees[y][x]);
    let rscore = get_score(&right_part, trees[y][x]);
    let tscore = get_score(&top_part, trees[y][x]);
    let bscore = get_score(&bot_part, trees[y][x]);
    Node {
      score : lscore * rscore * tscore * bscore,
      visible : vleft || vright || vtop || vbot
    }
    
}

fn get_visible(trees: &Vec<u8>, value: u8) -> bool {
    !trees.iter().any(|&e| e >= value)
}

fn get_score(trees: &Vec<u8>, value: u8) -> u64 {
    let mut score: u64 = 0;
    for (_pos, tree) in trees.into_iter().enumerate() {
        if tree < &value {
            score += 1;
        } else {
            score += 1;
            break;
        }
    }
    score
}
