FILE_PATH = "./input.txt"

# Part one
File.open(FILE_PATH, "r") do |f|
  sum = 0
  f.each_line do |line|
    first_half, second_half = line.chars.each_slice(line.length / 2).map(&:join)
    duplicate = first_half.chars.uniq.select { |char| second_half.include? char }.join.ord
    sum += duplicate <= 90 ? duplicate - 38 : duplicate - 96
  end
  puts sum
end 

# Part two
File.open(FILE_PATH, "r") do |f|
  sum = 0
  f.each_slice(3) do |lines|
    (fr, sr, tr) = lines.map{ |row| row.strip.bytes}
    badge = (fr.intersection(sr, tr)).join.to_i
    sum += badge <= 90 ? badge - 38 : badge - 96
  end
  puts sum
end


