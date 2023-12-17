start_index = []
$pipes_grid = []
$directions = { "S" => ["T", "B", "L", "R"], "|" => ["T", "B"], "-" => ["L", "R"], "L" => ["T", "R"], "J" => ["T", "L"], "7" => ["L", "B"], "F" => ["R", "B"], "." => [] }
File.open("./input.txt", "r") do |f|
    f.each_line do |l|
      new_row = l.chomp.chars.map{|c| c}
      $pipes_grid << new_row
      if new_row.include? "S"
        start_index << $pipes_grid.length - 1
        start_index << new_row.index(new_row.find { |l| l == "S"})
      end
    end
end

# p1
def check_path(start_position)
  stack = [[start_position, [], 0, start_position]]
  path = []

  while !stack.empty?
    position, current_path, depth, previous = stack.pop
    y, x = position
    pipe = $pipes_grid[y][x]

    if pipe != "S"
      depth += 1
    end

    dirs = $directions[pipe]
    [["T", -1, 0, "B"], ["B", 1, 0, "T"], ["L", 0, -1, "R"], ["R", 0, 1, "L"]].each do |dir, dy, dx, opposite|
      if dirs.include?(dir) && y + dy >= 0 && y + dy < $pipes_grid.length && x + dx >= 0 && x + dx < $pipes_grid[y].length
        new_pos = [y + dy, x + dx]
        next_pipe = $pipes_grid[new_pos[0]][new_pos[1]]
        is_compatible = $directions[next_pipe].include?(opposite) && previous != new_pos && !current_path.include?(new_pos)

        if is_compatible
          stack.push([new_pos, current_path + [new_pos], depth, position])
        end
      end
    end

    path = current_path if stack.empty?
  end

  path
end

path = check_path(start_index)
# p path
p (path.length / 2.0).ceil

# p2
$pipes_grid.each_with_index do |r, ri|
  r.each_with_index do |c, ci|
    if c == "S"
      r[ci] = "╝"
      next
    end
    if !path.include? [ri, ci]
      r[ci] = "."
      next
    end
    if c == "L"
      r[ci] = "╚"
    elsif c == "J" 
      r[ci] = "╝"
    elsif c == "F"
      r[ci] = "╔"
    elsif c == "|"
      r[ci] = "║"
    elsif c == "7"
      r[ci] = "╗"
    elsif c == "-"
      r[ci] = "═"
    end
  end
end


count = 0
pattern = /╚═*╗|╔═*╝|\║/
$pipes_grid.each_with_index do |row, row_index|
  row.each_with_index do |cell, index|
    if path.include? [row_index, index]
      next
    end
    c = row[0, index].join.scan(pattern).length
    if c % 2 == 1
      row[index] = "#"
      count += 1
    end
  end
end

puts count
$pipes_grid.each do |r|
  p r.join
end
