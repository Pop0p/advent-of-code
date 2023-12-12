FILE_PATH = "./input.txt"
POINTS = dict.fromkeys(["A", "X"], 1) | dict.fromkeys(["B", "Y"], 2) | dict.fromkeys(["C", "Z"], 3)
LOSES = { "A" : "Z", "B": "X", "C": "B"}
WINS = { "X" : "C", "Y" : "A", "Z" : "B"}

with open(FILE_PATH) as f:
    score_one = score_two = 0

    for line in f:
        opponent, myself = line.split()

        # Part one
        if POINTS.get(myself) == POINTS.get(opponent):
            score_one += 3
        elif opponent in WINS.get(myself):
            score_one += 6
        score_one += POINTS.get(myself)

        # Part two
        if myself == "X":
            myself = LOSES.get(opponent)
        elif myself == "Y":
            myself = opponent
            score_two += 3
        else:
            myself = [key for key, value in WINS.items() if value == opponent][0]
            score_two += 6

        score_two += POINTS.get(myself)

    print(score_one)
    print(score_two)
