FILE_PATH = "./input.txt"

$dirs = ["N", "S", "W", "E"]
$elves = Hash.new
# Parse file
File.open(FILE_PATH, "r") do |f|
  f.each_line.with_index do |line, y|
   line.split("").each.with_index do |char, x|
      if char == "#"
        $elves[x.to_s + ":" + y.to_s] = "?"
      end
    end
  end
end

has_no_move = false
turn = 0
while !has_no_move do
  $elves_b = Hash.new
  moving_to = {}
  $elves.each do |key, value| 
    position = key.split(":").map(&:to_i)
    pos_north = "#{position[0]}:#{position[1] - 1}"
    pos_north_east = "#{position[0] + 1}:#{position[1] - 1}"
    pos_north_west = "#{position[0] - 1}:#{position[1] - 1}"
    pos_south = "#{position[0]}:#{position[1] + 1}"
    pos_south_west = "#{position[0] - 1}:#{position[1] + 1}"
    pos_south_east = "#{position[0] + 1}:#{position[1] + 1}"
    pos_east = "#{position[0] + 1}:#{position[1]}"
    pos_west = "#{position[0] - 1}:#{position[1]}"

    north = $elves.key?(pos_north) || $elves.key?(pos_north_east) || $elves.key?(pos_north_west)
    south = $elves.key?(pos_south) || $elves.key?(pos_south_east) || $elves.key?(pos_south_west)
    west = $elves.key?(pos_west) || $elves.key?(pos_south_west) || $elves.key?(pos_north_west)
    east = $elves.key?(pos_east) || $elves.key?(pos_north_east) || $elves.key?(pos_south_east)

    
    if !north and !east and !west and !south
      $elves_b[key] = value
      next
    end

    moved = false 
    $dirs.each.with_index do |direction, index|
      if direction == "N" and !north
        if !moving_to.key? pos_north
          $elves_b[key] = pos_north
          moving_to[pos_north] = key
          moved = true
        else
          $elves_b[moving_to[pos_north]] = "?"
        end
        break
      end
      if direction == "S" and !south 
        if !moving_to.key? pos_south
          $elves_b[key] = pos_south
          moving_to[pos_south] = key
          moved = true
        else
          $elves_b[moving_to[pos_south]] = "?"
        end
        break
      end
      if direction == "W" and !west 
        if !moving_to.key? pos_west
          $elves_b[key] =pos_west
          moving_to[pos_west] = key
          moved = true
        else
          $elves_b[moving_to[pos_west]] = "?"
        end
        break
      end
      if direction == "E" and !east 
        if !moving_to.key? pos_east
          $elves_b[key] = pos_east
          moving_to[pos_east] = key
          moved = true
        else
          $elves_b[moving_to[pos_east]] = "?"
        end
        break
      end
    end
    if !moved
      $elves_b[key] = value
    end
  end

  $elves.clear()

  has_one = false
  $elves_b.each do |key, value|
    pos = value
    if value != "?"
      has_one = true
      $elves[value] = "?"
    else
      $elves[key] = "?"
      pos = key
    end  
    position = pos.split(":").map(&:to_i)
  end
  if !has_one
    has_no_move = true
  end
  
  $dirs = $dirs.rotate(1)
  if turn == 9
    lowest_x =  $elves.min_by{|k,v| k.split(":")[0].to_i}[0].split(":")[0].to_i
    lowest_y =  $elves.min_by{|k,v| k.split(":")[1].to_i}[0].split(":")[1].to_i
    highest_x = $elves.max_by{|k,v| k.split(":")[0].to_i}[0].split(":")[0].to_i
    highest_y = $elves.max_by{|k,v| k.split(":")[1].to_i}[0].split(":")[1].to_i
    y = lowest_y.abs() + highest_y + 1 
    x = lowest_x.abs() + highest_x + 1
    puts "#{x * y - $elves.count}"
  end
  turn += 1
end
puts turn
