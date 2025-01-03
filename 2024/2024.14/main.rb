# Setup
data = File.readlines("./input.txt").map do |line|
  parts = line.split
  start_coords = parts[0].split("=")[1].split(",").map(&:to_i)
  move_coords = parts[1].split("=")[1].split(",").map(&:to_i)

  { start_x: start_coords[0], start_y: start_coords[1], move_x: move_coords[0], move_y: move_coords[1] }
end
positions = data.map do |entry|
  new_x = (entry[:start_x] + entry[:move_x] * 100) % 101
  new_y = (entry[:start_y] + entry[:move_y] * 100) % 103
  [new_x, new_y]
end

# Part one
quadrants = [
  ->(x, y) { x.between?(0, 49) && y.between?(0, 50) },
  ->(x, y) { x.between?(51, 100) && y.between?(0, 50) },
  ->(x, y) { x.between?(0, 49) && y.between?(52, 102) },
  ->(x, y) { x.between?(51, 100) && y.between?(52, 102) }
]
result = quadrants.map { |q| positions.count { |x, y| q.call(x, y) } }.reduce(:*)
puts "Part one : #{result}"


# Part two
def points_aligned?(positions, count)
  positions.group_by { |x, y| y }.any? do |_y, group|
    xs = group.map(&:first).sort
    xs.each_cons(count).any? { |consecutive| consecutive.each_cons(2).all? { |a, b| b == a + 1 } }
  end
end

count = 1
loop do
  positions = data.map do |entry|
    new_x = (entry[:start_x] + entry[:move_x] * count) % 101
    new_y = (entry[:start_y] + entry[:move_y] * count) % 103
    [new_x, new_y]
  end

  if points_aligned?(positions, 15)
    puts "Part two : #{count}"
    break
  end

  count += 1
end
