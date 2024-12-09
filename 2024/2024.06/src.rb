require 'set'

lines = File.readlines("./input.txt").map { |line| line.chomp.chars }
width = lines.first.size
border = Array.new(width + 2, "@")
lines = [border] + lines.map { |line| ["@"] + line + ["@"] } + [border]
start_position_y, start_position_x = lines.each_with_index.map { |line, y| [y, line.index('^')] }.find { |_, x| x }
current_position_x = start_position_x
current_position_y = start_position_y
current_direction = 0
visited = Set.new

def add_visited_range(visited, range, fixed_pos, is_vertical)
  if range.first > range.last
    range.first.downto(range.last) do |pos|
      point = is_vertical ? [fixed_pos, pos] : [pos, fixed_pos]
      visited.add(point)
    end
  else
    range.each do |pos|
      point = is_vertical ? [fixed_pos, pos] : [pos, fixed_pos]
      visited.add(point)
    end
  end
end

def move(grid, visited, direction, x, y)
  deltas = {
    0 => { range: (y).downto(0), fixed: x, vertical: true, adjust: 1 },
    1 => { range: (x).upto(grid[y].size - 1), fixed: y, vertical: false, adjust: -1 },
    2 => { range: (y).upto(grid.size - 1), fixed: x, vertical: true, adjust: -1 },
    3 => { range: (x).downto(0), fixed: y, vertical: false, adjust: 1 }
  }

  params = deltas[direction]
  is_vertical = params[:vertical]
  obstacle, distance = nil, nil
  new_x, new_y = x, y

  params[:range].each do |pos|
    cell = is_vertical ? grid[pos][params[:fixed]] : grid[params[:fixed]][pos]
    if cell == "#" || cell == "+"
      obstacle = is_vertical ? [x, pos] : [pos, y]
      range_to_add = is_vertical ? (pos + params[:adjust])..y : (pos + params[:adjust])..x
      add_visited_range(visited, range_to_add, params[:fixed], is_vertical)
      new_y = is_vertical ? pos + params[:adjust] : y
      new_x = is_vertical ? x : pos + params[:adjust]
      break
    elsif cell == "@"
      range_to_add = is_vertical ? (pos + params[:adjust])..y : (pos + params[:adjust])..x
      add_visited_range(visited, range_to_add, params[:fixed], is_vertical)
    elsif cell == "^"
      range_to_add = is_vertical ? (pos + params[:adjust])..y : (pos + params[:adjust])..x
      add_visited_range(visited, range_to_add, params[:fixed], is_vertical)
    end
  end

  return [new_x, new_y, obstacle]
end

# part one
while (true)
  current_position_x, current_position_y, obstacle = move(lines, visited, current_direction, current_position_x, current_position_y)

  if obstacle.nil?
    puts(visited.length)
    break
  else
    current_direction = (current_direction + 1) % 4
  end
end

# part two
count = 0
fresh_visited = Set.new
visited.each_with_index do |position, index|
  next if position[0] == start_position_x && position[1] == start_position_y
  lines[position[1]][position[0]] = "+"
  current_direction = 0
  obstacle_hits_history = []
  current_position_x = start_position_x
  current_position_y = start_position_y
  is_looping = false

  while (true)
    current_position_x, current_position_y, obstacle = move(lines, fresh_visited, current_direction, current_position_x, current_position_y, )
    if obstacle.nil?
      break
    else
      if obstacle_hits_history.include? [obstacle, current_direction]
        is_looping = true
        break
      end
      obstacle_hits_history << [obstacle, current_direction]
      current_direction = (current_direction + 1) % 4
    end
  end

  lines[position[1]][position[0]] = "."
  if is_looping
    count += 1
  end
end

puts count