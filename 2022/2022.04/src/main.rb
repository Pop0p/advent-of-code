FILE_PATH = "./input.txt"


# Part one
File.open(FILE_PATH, "r") do |f|
  total = 0
  f.each_line do |line|
    fe, se = line.split(",").map { |sections| sections.split("-").map(&:to_i) }
    if (fe.first <= se.first && fe.last >= se.last) || (se.first <= fe.first && se.last >= fe.last)
      total += 1
    end
  end
    puts total
end

# Part Two
File.open(FILE_PATH, "r") do |f|
  total = 0
  f.each_line do |line|
    fe, se = line.split(",").map { |sections| sections.split("-").map(&:to_i) }
    if fe.first <= se.last && se.first <= fe.last
      total += 1
    end
  end
  puts total
end
