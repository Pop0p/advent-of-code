FILE_PATH = "./input.txt"

$paths = []
$size = [0,0]

def solve(two)
  File.open(FILE_PATH, "r") do | f |
    f.each_line do | line |
      $paths << line.split(" -> ").map { | el | el.split(",").map(&:to_i) }
    end
  end
  
  t = $paths.flatten(1).collect { |x| x[0] }.max
  $size[0] = t + 1
  t = $paths.flatten(1).collect{ |x| x[1] }.max
  $size[1] = t + 1
  
  if (two)
    # part two
    $paths << [[0, $size[1] + 1], [$size[0], $size[1] + 1]]
    t = $paths.flatten(1).collect{ |x| x[1] }.max
    $size[1] = t + 1
  end
  
  grid = Array.new($size[1]) {Array.new($size[0]) {"."}}
  $paths.each_with_index do | ps, x |
    ps.each_with_index do | path, i |
      next if ps[i + 1].nil?
      xa = path[0] 
      ya = path[1]
      xb = ps[i + 1][0]
      yb = ps[i + 1][1]
      if xa == xb
        (ya, yb = yb, ya) if ya > yb
        (ya..yb).each do | l |
          grid[l][xa] = "#"
        end
      elsif ya == yb
        (xa, xb = xb, xa) if xa > xb
        (xa..xb).each do | l |
          grid[ya][l] = "#"
        end
      end
    end
  end
  sand_overflow = false
  count = 0
  need_new = true
  while (!sand_overflow)
    if (need_new)
      x = 500
      y = 0
      need_new = false
    end



    grid[y][x] = "."
      
    if (y+1 == grid.length())
      sand_overflow = true;
      break;
    elsif (grid[y+1][x] == ".")
      grid[y+1][x] = "o"
      y += 1
    elsif (x-1 < 0) 
      if (two)
        grid.map { |x| x.unshift(".")};
        grid[grid.length() - 1][0] = "#"
        grid[y+1][x] = "o"
        y += 1
      else
        sand_overflow = true;
      break
      end
    elsif (grid[y+1][x-1] == ".")
      grid[y+1][x-1] = "o"
      y += 1
      x -= 1
    elsif (x+1 == grid[y+1].length())
      if (two)
        grid.map { |x| x.push(".")};
        grid[grid.length() - 1][-1] = "#"
        grid[y+1][x+1] = "o"
        y += 1
        x += 1
      else
        sand_overflow = true;
      break
      end
    elsif (grid[y+1][x+1] == ".")
      grid[y+1][x+1] = "o"
      y += 1
      x += 1
    else
      grid[y][x] = "o"
      count += 1
      if (x == 500 && y == 0 && two)
        sand_overflow = true
        break;
      end
      need_new = true
    end
  end
  puts count

end

solve(false)
solve(true)
