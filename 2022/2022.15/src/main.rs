use regex::Regex;
use std::collections::HashMap;
use std::fs;

struct Sensor {
    distance: i64,
    beacon: (i64, i64),
    border: Vec<(i64, i64)>,
}

const FILE_PATH: &str = "./input.txt";

fn main() {
    let content = fs::read_to_string(FILE_PATH).unwrap();
    let mut carte: HashMap<(i64, i64), Sensor> = HashMap::new();
    let re = Regex::new(r"x=(-?\d+),\sy=(-?\d+)").unwrap();
    for line in content.lines() {
        let report: Vec<(i64, i64)> = re
            .captures_iter(&line)
            .map(|c| (c[1].parse::<i64>().unwrap(), c[2].parse::<i64>().unwrap()))
            .collect();
        carte.insert(
            (report[0].0, report[0].1),
            Sensor {
                distance: manhattan_distance(&report[0], &report[1]),
                beacon: (report[1].0, report[1].1),
                border: Vec::new(),
            },
        );
    }
    part_one(&carte);
    part_two(&mut carte);
}

fn manhattan_distance(a: &(i64, i64), b: &(i64, i64)) -> i64 {
    (b.0 - a.0).abs() + (b.1 - a.1).abs()
}

fn part_one(carte: &HashMap<(i64, i64), Sensor>) {
    let y = 2000000;
    let mut segments: Vec<(i64, i64)> = Vec::new();
    /*
     * Get the segments that intersect with our sensors range
     * Simply substract the vertical distance between our sensor and y from our sensor range
     */
    for (key, value) in carte.iter() {
        let dx: i64 = value.distance - (key.1 - y).abs();
        if dx <= 0 {
            continue;
        }
        segments.push((key.0 - dx, key.0 + dx));
    }
    let aligned_beacons: Vec<i64> = carte
        .values()
        .filter(|x| x.beacon.1 == y)
        .map(|x| x.beacon.0)
        .collect();
    let mut count: i64 = 0;
    let min_x = *segments.iter().map(|(l, _)| l).min().unwrap();
    let max_x = *segments.iter().map(|(_, r)| r).max().unwrap();
    for x in (min_x)..(max_x) {
        if aligned_beacons.contains(&x) {
            continue;
        }

        for (_, r) in segments.iter().enumerate() {
            if r.0 <= x && x <= r.1 {
                count += 1;
                break;
            }
        }
    }
    println!("{}", count);
}

fn part_two(carte: &mut HashMap<(i64, i64), Sensor>) {
    for (key, value) in carte.iter_mut() {
        let dis = value.distance + 1; // get all points around the border.
        for d in (-dis)..(dis) {
            /*
             * get all points around the border of our diamond.
             * The loop iterates over the range -dis to dis,
             * and for each value of d, it adds two points to the list:
             * one with a positive x-coordinate and one with a negative x-coordinate.
             * The y-coordinate of each point is calculated as dis - abs(d),
             * which ensures that the Manhattan distance from the center (0, 0) is always 5.
             */
            let a = (d + key.0, dis - d.abs() + key.1);
            let b = (-d + key.0, -dis + d.abs() + key.1);

            if 0 < a.0 && a.0 <= 4000000 && 0 < a.1 && a.1 <= 4000000 {
                value.border.push(a);
            }
            if 0 < b.0 && b.0 <= 4000000 && 0 < b.1 && b.1 <= 4000000 {
                value.border.push(b);
            }
        }
    }
    for (key, value) in carte.iter() {
        for (x, y) in value.border.iter() {
            let not_found: bool = carte
                .iter()
                .filter(|&(k, v)| k != key && manhattan_distance(&(*x, *y), k) <= v.distance)
                .count()
                == 0;
            if not_found {
                println!("{}", x * 4000000 + y);
                return;
            }
        }
    }
}
